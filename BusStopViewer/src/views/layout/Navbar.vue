<template>
    <nav>
        <ul>
            <template v-if="!isLoggedIn">
                <li><router-link to="/register">Register</router-link></li>
                <li><router-link to="/login">Login</router-link></li>
            </template>
            <template v-else>
                <li><router-link to="/stops">All stops</router-link></li>
                <li><router-link to="/my-stops">My stops</router-link></li>
                <li>
                    <el-button @click="handleLogout">Logout</el-button>
                </li>
            </template>
        </ul>
    </nav>
</template>

<script lang="ts" setup>
import { computed } from 'vue';
import { useStore } from '../../store';

const store = useStore();

const isLoggedIn = computed(() => store.getters.isLoggedIn);
const handleLogout = () => {
    store.commit('logOut');
}

</script>