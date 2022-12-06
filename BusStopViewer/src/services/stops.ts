import request from './api';
import { Stop } from '../models/stop';
import { Delay } from '../models/delay';

export default {
    async fetchAllStops(): Promise<Stop[]> {
        const response = await request.get('/stop');
        return response.data
    },
    async fetchOwnedStops(): Promise<Stop[]> {
        const response = await request.get('/stop/assigned');
        return response.data
    },
    async assignStopToUser(stopId: number): Promise<any> {
        const response = await request.post(`/stop/${stopId}`);
        return response.data
    },
    async removeStopFromUser(stopId: number): Promise<any> {
        const response = await request.delete(`/stop/${stopId}`);
        return response.data
    },
    async fetchStopDelays(stopId: number): Promise<Delay[]> {
        const response = await request.get(`/stop/${stopId}`);
        return response.data
    }
}