import { createApp } from 'vue'
import App from './App.vue'
import router from './router';
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import store, { key } from './store';

createApp(App)
.use(router)
.use(store, key)
.use(ElementPlus)
.mount('#app')
