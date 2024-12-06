<template>
  <div class="flex flex-row w-full max-w-6xl mx-auto p-4 bg-white shadow-md rounded-lg">
    <!-- Form Section -->
    <div class="w-1/2 pr-4 border-r">
      <h2 class="text-lg font-bold mb-4">Sale Invoice</h2>
      <form @submit.prevent="submitForm" class="space-y-4">
        <!-- Invoice Date -->
        <div>
          <label for="invoiceDate" class="block text-sm font-medium mb-1">Invoice Date</label>
          <input
            id="invoiceDate"
            type="text"
            :value="form.invoiceDate"
            readonly
            class="w-full px-3 py-2 border rounded-lg bg-gray-100 text-gray-600"
          />
        </div>
        <!-- Customer -->
        <div>
          <label for="customerId" class="block text-sm font-medium mb-1">Customer</label>
          <select
            id="customerId"
            v-model="form.customerId"
            class="w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
          >
            <option value="" disabled>Select a customer</option>
            <option v-for="customer in customers" :key="customer.id" :value="customer.id">
              {{ customer.name }}
            </option>
          </select>
        </div>
        <!-- Submit and Remove Button -->
        <div class="flex space-x-2">
          <button
            type="submit"
            class="w-full px-4 py-2 bg-blue-500 text-white font-bold rounded-lg hover:bg-blue-600"
          >
            Submit
          </button>
          <!-- Remove Button (visible only after submit) -->
          <button
            v-if="submitted"
            @click="removeForm"
            type="button"
            class="w-full px-4 py-2 bg-red-500 text-white font-bold rounded-lg hover:bg-red-600"
          >
            Remove
          </button>
        </div>
      </form>
      
      <!-- Cart Section (moved below the form) -->
      <div v-if="cart.length > 0" class="mt-8">
        <h2 class="text-lg font-bold mb-4">Cart</h2>
        <ul>
          <li
            v-for="(item, index) in cart"
            :key="index"
            class="flex items-center justify-between p-2 border-b"
          >
            <div class="flex items-center">
              <img :src="item.productImage" alt="" class="w-12 h-12 mr-4" />
              <span>{{ item.productName }}</span>
            </div>
            <div class="flex items-center space-x-2">
              <button @click="updateQuantity(item, -1)" class="px-2 py-1 bg-gray-200 rounded">-</button>
              <span>{{ item.quantity }}</span>
              <button @click="updateQuantity(item, 1)" class="px-2 py-1 bg-gray-200 rounded">+</button>
              <button @click="removeFromCart(item)" class="px-2 py-1 bg-red-500 text-white rounded">Remove</button>
            </div>
          </li>
        </ul>

        <!-- Total Price Section -->
        <div class="flex justify-between items-center mt-4">
          <span class="text-xl font-bold">Total Price:</span>
          <span class="text-xl font-bold text-green-500">{{ totalPrice }}</span>
        </div>

        <!-- Sale Button -->
        <button
          v-if="cart.length > 0"
          @click="processSale"
          class="w-full mt-4 px-4 py-2 bg-green-500 text-white font-bold rounded-lg hover:bg-green-600"
        >
          Complete Sale
        </button>
      </div>
    </div>

    <!-- Product List Section (moved to the right) -->
    <div v-if="showProducts" class="w-1/2 pl-4">
      <h2 class="text-lg font-bold mb-4">Products</h2>
      <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
        <div
          class="shadow-lg rounded-lg p-4"
          v-for="(product, index) in products"
          :key="index"
        >
          <img :src="product.productImage" alt="" class="w-32 h-32 mx-auto" />
          <h3 class="text-center mt-2">{{ product.productName }}</h3>
          <p class="text-center text-sm text-gray-600">Quantity: {{ product.quantity }}</p>
          <p class="text-center text-sm text-gray-800 font-semibold">Price: ${{ product.productPrice }}</p> <!-- Giá sản phẩm -->
          <button
            class="w-full mt-2 bg-green-500 text-white py-1 rounded-lg hover:bg-green-600"
            @click="addToCart(product)"
          >
            Add to Cart
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      form: {
        invoiceDate: new Date().toLocaleString(),
        customerId: null,
      },
      customers: [
        { id: "C001", name: "John Doe" },
        { id: "C002", name: "Jane Smith" },
        { id: "C003", name: "Alice Johnson" },
      ],
      products: [
        { productId: 1, productName: "Product A", quantity: 10, productPrice: 100, productImage: "image-a.jpg" },
        { productId: 2, productName: "Product B", quantity: 5, productPrice: 200, productImage: "image-b.jpg" },
        { productId: 3, productName: "Product C", quantity: 2, productPrice: 300, productImage: "image-c.jpg" },
      ],
      cart: [], // Giỏ hàng
      showProducts: false,
      submitted: false, // Đánh dấu khi form đã được submit
    };
  },
  computed: {
    totalPrice() {
      return this.cart.reduce((total, item) => total + (item.productPrice * item.quantity), 0).toFixed(2);
    }
  },
  methods: {
    submitForm() {
      if (!this.form.customerId) {
        alert("Please select a customer.");
        return;
      }
      this.showProducts = true;
      this.submitted = true; // Đánh dấu form đã được submit
      console.log("Form submitted:", this.form);
    },
    addToCart(product) {
      const existingItem = this.cart.find((item) => item.productId === product.productId);
      if (existingItem) {
        existingItem.quantity++;
      } else {
        this.cart.push({ ...product, quantity: 1 });
      }
    },
    updateQuantity(item, amount) {
      const updatedQuantity = item.quantity + amount;
      if (updatedQuantity <= 0) {
        this.removeFromCart(item);
      } else {
        item.quantity = updatedQuantity;
      }
    },
    removeFromCart(item) {
      this.cart = this.cart.filter((cartItem) => cartItem.productId !== item.productId);
    },
    processSale() {
      if (this.cart.length > 0) {
        alert("Sale completed!");
        // Here you can handle the sale logic (e.g., saving the sale, updating inventory, etc.)
        this.cart = []; // Clear the cart after sale
      }
    },
    removeForm() {
      this.form.customerId = null;
      this.submitted = false;
      this.cart = []; // Clear the cart when remove is clicked
      this.showProducts = false;
    }
  },
};
</script>

<style scoped>
/* Add custom styles here */
</style>
