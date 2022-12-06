const state = {
    isLoggedIn: false
}

const mutations = {
    logIn(state:{ isLoggedIn:boolean }, token: string) {
        state.isLoggedIn = true;
        localStorage.setItem('jwt', token);
    },
    logOut(state:{ isLoggedIn:boolean}) {
        state.isLoggedIn = false;
        localStorage.clear();
    }
}

const getters = {
    isLoggedIn(state:{ isLoggedIn:boolean }) {
        return state.isLoggedIn;
    }
}

export default {
    state,
    mutations,
    getters
}