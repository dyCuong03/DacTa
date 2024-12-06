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
            :value="invoiceDate"
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
            <option v-for="customer in customers" :key="customer.customerId" :value="customer.customerId">
              {{ customer.customerName }}
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
              <img :src="item.image" alt="" class="w-12 h-12 mr-4" />
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
          <img :src="product.image" alt="" class="w-32 h-32 mx-auto" />
          <h3 class="text-center mt-2">{{ product.productName }}</h3>
          <p class="text-center text-sm text-gray-600">Quantity: {{ product.quantity }}</p>
          <p class="text-center text-sm text-gray-800 font-semibold">Price: ${{ product.salePrice }}</p> <!-- Giá sản phẩm -->
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
  import { mapState, mapActions } from "vuex";
  import axiosClient from "../../axiosClient";
export default {
  data() {
    return {
      invoiceDate:  new Date().toISOString(),
      form:{
        employeeId: '',
        customerId: '',
      },
      cart: [], 
      showProducts: false,
      submitted: false, 
      totalPrice:0
    };
  },
  computed: {
    ...mapState(["products", "customers", "user"]),
    
  },
  watch: {
    cart: {
  handler(newCart) {
    console.log("Cart updated:", newCart); // Kiểm tra dữ liệu giỏ hàng

    this.totalPrice = newCart.reduce((acc, element) => {
      if (!element.salePrice || !element.quantity) {
        console.warn("Invalid product data:", element);
      console.log(element)

      }
      return acc + ((Number(element.salePrice) || 0) * (Number(element.quantity) || 0));
    }, 0);

    console.log("Total Price:", this.totalPrice); // Kiểm tra tổng tiền
  },
  deep: true,
},
},

    
  async mounted(){
    await this.GetProducts();
    await this.GetCustomers().then(() => {
      console.log("Customers loaded:", this.customers);
    });
    if(this.user){
    this.form.employeeId = this.user.id;
    }
   
  },

  methods: {
    total() {
    this.totalPrice = this.cart.reduce((acc, element) => {
        return acc + (element.productPrice * element.quantity);
    }, 0);

       
      
                           
    },
    ...mapActions(["GetProducts", "GetCustomers"]),
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
    async processSale() {
    console.log("Processing sale:", this.form);

    // Kiểm tra xem giỏ hàng có sản phẩm không
    if (this.cart.length > 0) {
        try {
            // Loại bỏ SalesInvoiceId nếu có
            var saleInvoiced = {
              employeeId: this.form.employeeId,
              customerId: this.form.customerId,
              totalAmount: this.totalPrice
            };
            console.log(saleInvoiced)

            const response = await axiosClient.post('SaleInvoice/AddSaleInvoice', saleInvoiced);

            if (response.status === 200) {
                // Gửi chi tiết bán
                for (const element of this.cart) {
                   const saledetail = {
                      salesInvoiceId: response.data.salesInvoiceId,
                        productId: element.productId,
                        amount: element.quantity  ,
                    };
                    console.log("Sale detail:", saledetail);
                    const saleDetailResponse = await axiosClient.post('SaleDetails/AddSaleDetail', saledetail);
      
                }
                        alert("Sale completed!");
                        this.cart = [];  // Giỏ hàng trống sau khi bán
            }
        } catch (error) {
            console.error("Error:", error);
            alert("An error occurred while processing the sale. Please try again.");
        }
    }
  },


    removeForm() {
      this.form.customerId = null;
      this.submitted = false;
      this.cart = [];
      this.showProducts = false;
    }
  },
};
</script>

<style scoped>
</style>
