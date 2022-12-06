import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Register from "../views/Register.vue";
import Login from "../views/Login.vue";

const routes: RouteRecordRaw[] = [
    {
        path: '/register',
        component: Register
    },
    {
        path: '/login',
        component: Login
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes
  });

export default router;  