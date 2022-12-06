<template>
    <input :value="modelValue" @input="handleInput($event)" :placeholder="placeholder"/>
</template>
<script lang="ts" setup>
const emit = defineEmits(['update:modelValue']);
defineProps({
    modelValue: {
        type: String,
        required: false,
    },
    placeholder: {
        type: String,
        required: false
    }
})

const handleInput = (e: any) => {
    emit('update:modelValue', e.target.value);
}

</script>