import { InjectionKey } from 'vue';
import { createStore, Store, useStore as baseUseStore } from 'vuex'

import auth from './auth';
import stops from './stops';

export const key: InjectionKey<Store<any>> = Symbol();

export const useStore = () => {
  return baseUseStore(key)
}

export default createStore({
  modules: {
    auth,
    stops
  }
});