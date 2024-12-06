<template>
  <div class="flex flex-row justify-between my-8 w-full max-w-6xl">
    <h2 class="text-xl">Products</h2>
  </div>
  <div class="flex flex-col items-center justify-center min-h-screen px-5">
  <div class="grid grid-flow-row md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-3 gap-10 max-w-6xl" v-if="products">
    <div class="shadow-lg rounded-lg w-70" v-for="(product, index) in products" :key="index">
      <p class="flex justify-center w-full" ><img :src="product.productImage" alt="" class="w-44 h-40" @click="ProductView(product.productId)"></p>
      <div class="p-6 flex justify-center w-full">
        <h3>{{ product.productName }}</h3>
        
      </div>
      <div class="flex justify-center">
      <h4>Quantity : {{ product.quantity }}</h4>
        
      </div>
     
      <div class="flex flex-row justify-between items-center m-5 space-x-4 p-2">
        <p 
          v-if="user" 
          class="bg-gradient-to-r from-red-600 to-pink-500 rounded-full py-1 px-2 text-gray-50 text-sm flex items-center hover:from-pink-600 hover:to-red-600"
          @click="addToCart(product)">
          <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-4 h-4 mr-1">
            <path stroke-linecap="round" stroke-linejoin="round" d="M2.25 3h1.386c.51 0 .955.343 1.087.835l.383 1.437M7.5 14.25a3 3 0 0 0-3 3h15.75m-12.75-3h11.218c1.121-2.3 2.1-4.684 2.924-7.138a60.114 60.114 0 0 0-16.536-1.84M7.5 14.25 5.106 5.272M6 20.25a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0Zm12.75 0a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0Z" />
          </svg>
          Add to cart
          </p>

          <p 
          v-else 
          class="bg-gray-300 rounded-full py-1 px-2 text-gray-500 text-sm flex items-center cursor-not-allowed">
          <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-4 h-4 mr-1">
            <path stroke-linecap="round" stroke-linejoin="round" d="M2.25 3h1.386c.51 0 .955.343 1.087.835l.383 1.437M7.5 14.25a3 3 0 0 0-3 3h15.75m-12.75-3h11.218c1.121-2.3 2.1-4.684 2.924-7.138a60.114 60.114 0 0 0-16.536-1.84M7.5 14.25 5.106 5.272M6 20.25a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0Zm12.75 0a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0Z" />
          </svg>
          Add to cart (Login required)
          </p>

        <p class="bg-gradient-to-r from-purple-600 to-purple-500 rounded-full py-1 px-2 text-gray-50 text-sm flex items-center hover:from-purple-500 hover:to-purple-600">
          <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-4 h-4 mr-1">
            <path stroke-linecap="round" stroke-linejoin="round" d="M13.5 4.5 21 12m0 0-7.5 7.5M21 12H3" />
          </svg>
          View Details
        </p>
      </div>
    </div>

     <!-- Modal -->
<div v-if="showModal" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
<div class="bg-white p-5 rounded-lg w-96">
  <h3 class="text-xl mb-4">Thêm sản phẩm vào giỏ hàng</h3>

  <!-- Hiển thị ảnh sản phẩm -->
  <div class="flex justify-center mb-4">
    <img :src="selectedProduct.productImage" alt="Product Image" class="w-32 h-32 object-cover">
  </div>

  <!-- Hiển thị tên sản phẩm -->
  <p><strong>Sản phẩm:</strong> {{ selectedProduct.productName }}</p>

  <!-- Tăng/Giảm số lượng -->
  <div class="flex items-center justify-between mt-4">
    <p>Số lượng:</p>
    <div class="flex items-center space-x-4">
      <button 
        class="bg-gray-300 px-2 py-1 rounded" 
        @click="decreaseQuantity" 
        :disabled="quantity <= 1">
        -
      </button>
      <span>{{ quantity }}</span>
      <button 
        class="bg-gray-300 px-2 py-1 rounded" 
        @click="increaseQuantity">
        +
      </button>
    </div>
  </div>

  <!-- Nút hành động -->
  <div class="flex justify-between mt-4">
    <button class="bg-red-500 text-white px-4 py-2 rounded" @click="closeModal">Hủy</button>
    <button class="bg-green-500 text-white px-4 py-2 rounded" @click="confirmAddToCart">Xác nhận</button>
  </div>
</div>
</div>
  </div>
</div>


</template>

<script>
import { mapState, mapActions } from 'vuex'
import axiosClient from '../../axiosClient';
export default{
data(){
  return{
    showModal: false,
    selectedProduct: null,
    quantity: 1,
  }
},
computed: {
  ...mapState(['products', 'user', 'cart', 'cartdetail']),
},
async mounted() {
  await this.GetProducts().then(() => {
    console.log("Products loaded:", this.products);
  });
  if(this.user){
    await this.GetCart(this.user.id).then(() => {
      console.log("Car000000t loaded:", this.user);
    this.GetCartDetail(this.cart.orderId).then(() =>{
      console.log("CartDetail loaded:", this.cartdetail);
    })
  });
  }
  
},
methods: {
  ...mapActions(['GetProducts', 'GetCart', 'GetCartDetail']),
  addToCart(product) {
    if (product) {
      this.selectedProduct = product;
      console.log("Selected product:", this.selectedProduct);
      this.showModal = true;
    } else {
      console.error("Product is not available.");
    }
  },

  closeModal() {
    this.showModal = false;
    this.selectedProduct = null;
  },
  async confirmAddToCart() {
    const cartItem = {
      orderId: this.cart.orderId,
      productId: this.selectedProduct.productId,
      quantity: this.quantity,
  }
try {
  console.log("Cart item:", cartItem);
  const response = await axiosClient.post(`OrderDetail/AddCartDetail`, cartItem);
  if (response.status === 200) {
    alert(`Đã thêm sản phẩm: ${this.selectedProduct.productName} vào giỏ hàng!`);
  }
} catch (error) {
  if (error.response) {
    switch (error.response.status) {
      case 400:
        alert("Số lượng không hợp lệ!");  
        break;
      case 409:
        const detail = this.cartdetail.find(detail => detail.orderId === this.cart.orderId && detail.productId ===  this.selectedProduct.productId);
        console.log("Cart ___detail:", this.cartdetail);
        var cardetailUP = {
          orderId: detail.orderId,
          productId: this.selectedProduct.productId,
          quantity: detail.quantity + this.quantity,
        };
        console.log("Cart detail update:", cardetailUP);
        try {
          const res = await axiosClient.put(`OrderDetail/UpdateCartDetail`, cardetailUP);
          if (res.status === 200) {
            alert(`Đã cập nhật sản phẩm: ${this.selectedProduct.productName} vào giỏ hàng!`);
          }
        } catch (updateError) {
          if (updateError.response) {
            switch (updateError.response.status) {
              case 409:
                alert("Sản phầm đã hết hàng");
                break;
              case 400:{
                alert("Bạn đã thêm vào giỏ quá số lượng mặt hàng còn lại");
                break;
              }
              default:
                alert("Đã xảy ra lỗi không xác định!");
            }
          }
        }
        break;
      default:
        alert("Đã xảy ra lỗi không xác định!");
    }
  } else {
    alert("Không thể kết nối đến máy chủ. Vui lòng thử lại!");
  }
} finally {
  this.closeModal();
}
},
  increaseQuantity() {
    this.quantity += 1;
  },
  decreaseQuantity() {
    if (this.quantity > 1) {
      this.quantity -= 1;
    }
  },
}

}
</script>