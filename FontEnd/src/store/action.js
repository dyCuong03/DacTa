import axiosClient from "../axiosClient";

export async function GetUser({ commit}, userName){
    try {
        const response = await axiosClient.get(`Authentication/GetUser?userName=${userName}`);
        commit('Set_User', response.data);
        console.log(response);
    } catch (error) {
        console.log(error);
    }
}

export async function GetProducts({ commit}){
    try {
        const response = await axiosClient.get(`Product/GetProducts`);
        commit('Set_Products', response.data.$values);
    } catch (error) {
        console.log(error);
    }
}
