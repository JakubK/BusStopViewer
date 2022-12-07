<template>
    <div>
        <h2>Register form</h2>
        <label>Email</label>
        <input v-model="form.email" type="text"/>

        <label>Password</label>
        <input v-model="form.password" type="text"/>

        <button @click="handleLogin" type="button">Submit</button>
    </div>
</template>

<script lang="ts" setup>
import { Ref, ref } from 'vue';
import authService from '../services/auth';
import { LoginForm } from '../models/login';
import { useStore } from '../store';

const form:Ref<Partial<LoginForm>> = ref({});

const store = useStore();
const handleLogin = async() => {
    const token = await authService.login(form.value as LoginForm);
    store.commit('logIn', token)
}

</script>