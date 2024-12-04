import { createRouter, createWebHistory } from 'vue-router'
import MainLayout from '../views/layouts/master.vue';
import Home from '../views/home.vue'
import Login from '../views/login.vue'
import Product from '../views/client/product.vue'
import Sale from '../views/client/sale.vue'
import Purchase from '../views/client/purchase.vue'
const routes = [
    {
        path: '/',
        name: 'mainlayout',
        component: MainLayout,
        children: [
            {
                path: '/',
                name: '/',
                component: Home
            },
            {
                path: '/login',
                name: 'login',
                component: Login
            },
            {
                path: '/product',
                name: 'product',
                component: Product
            },
            {
                path: '/sale',
                name: 'sale',
                component: Sale
            },
            {
                path: '/purchase',
                name: 'purchase',
                component: Purchase
            },
        ]
    }
]
const router = createRouter({
    history: createWebHistory(),
    routes,
});
export default router;
