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
          <option v-for="supplier in suppliers" :key="supplier.supplierId" :value="supplier.supplierId">
            {{ supplier.supplierName }}
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
              @change="updateTotalAmount"  
            >
              <option value="" disabled>Select a product</option>
              <option v-for="productOption in products" :key="productOption.productId" :value="productOption.productId">
                {{ productOption.productName }}
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
              @input="updateTotalAmount"  
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
      <!-- Total Amount -->
      <div class="mt-4">
        <span class="font-bold">Total Amount: </span>{{ totalAmount }}
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
import axiosClient from '../../axiosClient';
import { mapState } from 'vuex';

export default {
  data() {
    return {
      form: {
        invoiceDate: new Date().toLocaleString(), // Set thời gian hiện tại
        supplierId: null,
        products: [], // Danh sách sản phẩm được thêm
      },
      suppliers: [],
      products: [],  // Product data fetched from the API
      totalAmount: 0,  // Tổng số tiền
    };
  },
  computed: {
    ...mapState(['user']),
  },
  mounted() {
    this.fetchSuppliers();
    this.fetchProduct();
  },
  methods: {
    async fetchSuppliers() {
      try {
        const response = await axiosClient.get('Supplier/GetSupplier');
        this.suppliers = response.data.$values;
      } catch (error) {
        console.error('Error fetching suppliers:', error);
      }
    },
    async fetchProduct() {
      try {
        const response = await axiosClient.get('/Product/GetProducts');
        this.products = response.data.$values;
      } catch (error) {
        console.error('Error fetching products:', error);
      }
    },
    addProductRow() {
      this.form.products.push({ productId: "", quantity: 1, price: 0 });
      this.updateTotalAmount();  // Update total when a new product is added
    },
    removeProductRow(index) {
      this.form.products.splice(index, 1);
      this.updateTotalAmount();  // Update total after removing a product
    },
    updateTotalAmount() {
      this.totalAmount = this.form.products.reduce((total, product) => {
        const productDetails = this.products.find(p => p.productId === product.productId);
        const productPrice = productDetails ? productDetails.salePrice : 0;  // Using SalePrice from API
        return total + (product.quantity * productPrice);  // Calculate total
      }, 0);
    },
    async submitForm() {
      if (!this.form.supplierId) {
        alert("Please select a supplier.");
        return;
      }
      if (this.form.products.length === 0) {
        alert("Please add at least one product.");
        return;
      }
      const purchaseInvoiceDto = {
        PurchaseInvoiceId: "",
        InvoiceDate: new Date().toISOString(),
        TotalAmount: this.totalAmount, 
        EmployeeId: this.user.id,
      };

      console.log("Submitting form with payload:", purchaseInvoiceDto); 

      try {
        var response = await axiosClient.post('PurchaseInvoice/AddPurchaseInvoice', purchaseInvoiceDto);
        this.form.products.forEach(async element =>  {
          const purchaseDetail = {
            purchaseInvoiceId: response.data.purchaseInvoiceId,
            productId : element.productId,
            amount : element.quantity,
            supplierId : this.form.supplierId
        };
        console.log(purchaseDetail);
            await axiosClient.post('PurchaseDetails/AddPurchaseDetail', purchaseDetail);
        });
      } catch (error) {
        console.error('Error submitting form:', error);
      }
    },
  },
};
</script>

<style scoped>
/* Custom styling if needed */
</style>
