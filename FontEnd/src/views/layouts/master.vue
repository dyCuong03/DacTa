<template>
    <div class="{'w-full h-screen bg-gray-300'}">
        <HeaderComponent />
        <!-- <div class="flex flex-row justify-between items-center pt-10">
            <div class="w-1/2 pt-6 md:block">
                <SearchComponent v-if="!isHomePage && !isLoginPage && !isRegisterPage && !isChangePasswordPage && !isSendEmailPage && !isVerifyOtpPage" @search-results="handleSearchResults" />
            </div>
            <div class="w-1/2 hidden md:block">
                <SideBarComponent />
            </div>
        </div> -->
        
        <div>
            <router-view />
        </div>
        <FooterComponent/>
    </div>
</template>

<script>
import { computed } from 'vue';
import { useRoute } from 'vue-router';
import FooterComponent from '../../components/FooterComponent.vue';
import HeaderComponent from '../../components/HeaderComponent.vue';
export default {
    components: {
        HeaderComponent,
        FooterComponent,
    },
    setup() {
        
    },
}
</script>
