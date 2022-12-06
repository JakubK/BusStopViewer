import { LoginForm } from '../models/login';
import { RegisterForm } from '../models/register';
import request from './api';

export default {
    async register(payload: RegisterForm): Promise<any>
    {
        const response = await request.post('/auth/register', payload);
        return response.data;
    },
    async login(payload: LoginForm): Promise<string>
    {
        const response = await request.post('/auth/login', payload);
        return response.data;
    }
}