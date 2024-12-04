<template>
    <!-- begin hedaer -->
<div class="containner h-8 mx-auto p-5 w-full">
  <div class="md:flex md:flex-row md:justify-between text-center">
      <div class="flex flex-row justify-center">
          <div class="bg-gradient-to-r from-purple-600 to-red-600 h-8 w-8 rounded-lg"> </div>
          <h1 class="text-gray-600 text-xl ml-2">VirGo</h1>
      </div>
      <div class="mt-2">
          <router-link :to="{name:'/'}" class=" text-gray-600 hover:text-purple-400 p-4">Home</router-link>
          <router-link :to="{name:'product'}" class=" text-gray-600 hover:text-purple-400 p-4">Product</router-link>
          <router-link :to="{name:'sale'}" class=" text-gray-600 hover:text-purple-400 p-4">Sale</router-link>
          <router-link :to="{name:'purchase'}" class=" text-gray-600 hover:text-purple-400 p-4">Purchase</router-link>

          <router-link v-if="!user" :to="{name:'login'}" class=" text-gray-600 hover:text-purple-400 p-4">Login</router-link>
              <span v-if="user" @click="toggle" class="text-gray-600 hover:text-purple-400 p-4">
              <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-6 inline-block">
                  <path stroke-linecap="round" stroke-linejoin="round" d="M17.982 18.725A7.488 7.488 0 0 0 12 15.75a7.488 7.488 0 0 0-5.982 2.975m11.963 0a9 9 0 1 0-11.963 0m11.963 0A8.966 8.966 0 0 1 12 21a8.966 8.966 0 0 1-5.982-2.275M15 9.75a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z" />
              </svg>
              <Menu id="overlay_menu" ref="menu" :model="items" :popup="true" />
          </span>
         
      </div>
  </div>
</div>



<!-- end header -->
</template>

<script>
import { mapState, mapActions } from 'vuex';
export default {
    data(){
        return {
            value: null,
            items: [
                {
                    label: 'Profile',
                    icon: 'pi pi-user',
                    command: () => {
                        this.$router.push('/profile');
                    }
                },
                {
                    label: 'Logout',
                    icon: 'pi pi-sign-out',
                    command: () => {
                       // localStorage.removeItem('token');
                       // this.$store.dispatch('logout'); 
                        this.$router.push('/login');
                        this.$store.commit('Set_Username', null)
                        this.$store.commit('Set_User', null)
                        
                        
                    }
                }
            ]
        };
    },
    computed:{
        ...mapState(['username', 'user']),
    },
    watch:{
        username: {
            handler(newUserName) {
                if (newUserName) {
                    this.GetUser(newUserName);
                }
            }
        }
    },
    
    async created() {
        if(this.username){
            await this.GetUser(this.username);
            console.log("user:", this.user);
        }
    },
    methods:{
        ...mapActions(['GetUser']),
        toggle(event) {
           this.$refs.menu.toggle(event);
        },
    }
}
</script>