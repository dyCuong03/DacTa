<template>
  <div class="flex flex-col w-full max-w-3xl mx-auto p-4 bg-white shadow-md rounded-lg">
    <!-- Form Title -->
    <h2 class="text-lg font-bold mb-2">Purchase Invoice</h2>
    <!-- Form -->
    <form @submit.prevent="submitForm" class="space-y-4">
      <!-- Invoice Date (Read-only) -->
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
      <!-- Supplier (Combobox) -->
      <div>
        <label for="supplierId" class="block text-sm font-medium mb-1">Supplier</label>
        <select
          id="supplierId"
          v-model="form.supplierId"
          class="w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
        >
          <option value="" disabled>Select a supplier</option>
          <option v-for="supplier in suppliers" :key="supplier.id" :value="supplier.id">
            {{ supplier.name }}
          </option>
        </select>
      </div>
      <!-- Add Product Button -->
      <div>
        <button
          type="button"
          @click="addProductRow"
          class="w-full px-4 py-2 bg-green-500 text-white font-bold rounded-lg hover:bg-green-600"
        >
          Add Product
        </button>
      </div>
      <!-- Products Section -->
      <div v-for="(product, index) in form.products" :key="index" class="space-y-2">
        <div class="flex space-x-4 items-center">
          <!-- Product Combobox -->
          <div class="flex-1">
            <label :for="'product-' + index" class="block text-sm font-medium mb-1">Product</label>
            <select
              :id="'product-' + index"
              v-model="product.productId"
              class="w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
            >
              <option value="" disabled>Select a product</option>
              <option v-for="productOption in products" :key="productOption.id" :value="productOption.id">
                {{ productOption.name }}
              </option>
            </select>
          </div>
          <!-- Quantity Input -->
          <div class="flex-1">
            <label :for="'quantity-' + index" class="block text-sm font-medium mb-1">Quantity</label>
            <input
              :id="'quantity-' + index"
              type="number"
              v-model="product.quantity"
              class="w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
              placeholder="Enter quantity"
            />
          </div>
          <!-- Remove Product Button -->
          <button
            type="button"
            @click="removeProductRow(index)"
            class="px-3 py-2 bg-red-500 text-white rounded-lg hover:bg-red-600"
          >
            Remove
          </button>
        </div>
      </div>
      <!-- Submit Button -->
      <div class="mt-4">
        <button
          type="submit"
          class="w-full px-4 py-2 bg-blue-500 text-white font-bold rounded-lg hover:bg-blue-600"
        >
          Submit
        </button>
      </div>
    </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      form: {
        invoiceDate: new Date().toLocaleString(), // Set thời gian hiện tại
        supplierId: null,
        products: [], // Danh sách sản phẩm được thêm
      },
      suppliers: [
        { id: "S001", name: "Supplier A" },
        { id: "S002", name: "Supplier B" },
        { id: "S003", name: "Supplier C" },
      ],
      products: [
        { id: "P001", name: "Product A" },
        { id: "P002", name: "Product B" },
        { id: "P003", name: "Product C" },
      ],
    };
  },
  methods: {
    addProductRow() {
      this.form.products.push({ productId: "", quantity: null });
    },
    removeProductRow(index) {
      this.form.products.splice(index, 1);
    },
    submitForm() {
      if (!this.form.supplierId) {
        alert("Please select a supplier.");
        return;
      }
      if (this.form.products.length === 0) {
        alert("Please add at least one product.");
        return;
      }
      console.log("Form Submitted:", this.form);
      // Thực hiện hành động submit
    },
  },
};
</script>

<style scoped>
/* Tùy chỉnh thêm nếu cần */
</style>
