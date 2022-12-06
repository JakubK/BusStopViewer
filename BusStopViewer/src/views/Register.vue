<template>
    <div>
        <h2>Register form</h2>
        <label>Email</label>
        <input v-model="form.email" type="text"/>

        <label>Password</label>
        <input v-model="form.password" type="password"/>

        <label>Password confirm</label>
        <input v-model="form.passwordConfirm" type="password"/>

        <button @click="handleRegister" type="button">Submit</button>
    </div>
</template>

<script lang="ts" setup>
import { Ref, ref } from 'vue';
import { RegisterForm } from '../models/register';
import authService from '../services/auth';
import router from '../router';

const form:Ref<Partial<RegisterForm>> = ref({});

const handleRegister = async() => {
    await authService.register(form.value as RegisterForm);
    router.push('/login');
}

</script>