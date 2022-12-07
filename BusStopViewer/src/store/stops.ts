import { Stop } from "../models/stop"

const state = {
    stops: [],
    selectedStops: []
}

const mutations = {
    fillStops(state: {stops: Stop[]}, newStops: Stop[]) {
        state.stops = newStops
    },
    fillSelectedStops(state: {selectedStops: Stop[]}, newStop:Stop[]) {
        state.selectedStops = newStop;
    }
}

const getters = {
    getStops(state: {stops: Stop[]}) {
        return state.stops
    },
    getSelectedStops(state: {selectedStops: Stop[]}) {
        return state.selectedStops
    }
}

export default {
    state,
    mutations,
    getters
}