import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Register from "../views/Register.vue";
import Login from "../views/Login.vue";
import Stops from "../views/Stops.vue";
import AssignedStops from "../views/AssignedStops.vue";

const routes: RouteRecordRaw[] = [
    {
        path: '/register',
        component: Register
    },
    {
        path: '/login',
        component: Login
    },
    {
        path: '/stops',
        component: Stops
    },
    {
        path: '/my-stops',
        component: AssignedStops
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;  