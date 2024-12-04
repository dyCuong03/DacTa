import { createStore } from 'vuex';
import  state  from './state';
import * as mutations from './mutatitions';
import * as actions from './action';
const store = createStore({
    state,
    actions,
    mutations,
  });

export default store;