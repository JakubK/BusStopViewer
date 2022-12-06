import { Stop } from "../models/stop"

const state = {
    stops: [],
    selectedStop: null
}

const mutations = {
    fillStops(state: {stops: Stop[]}, newStops: Stop[]) {
        state.stops = newStops
    },
    fillSelectedStop(state: {selectedStop: Stop}, newStop:Stop) {
        state.selectedStop = newStop;
    }
}

const getters = {
    getStops(state: {stops: Stop[]}) {
        return state.stops
    },
    getSelectedStop(state: {selectedStop: Stop}) {
        return state.selectedStop
    }
}

export default {
    state,
    mutations,
    getters
}