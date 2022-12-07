import router from "../router";

const state = {
    isLoggedIn: false
}

const mutations = {
    logIn(state:{ isLoggedIn:boolean }, token: string) {
        state.isLoggedIn = true;
        localStorage.setItem('jwt', token);
        router.push('/stops');
    },
    logOut(state:{ isLoggedIn:boolean}) {
        state.isLoggedIn = false;
        localStorage.clear();
        router.push('/login');
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