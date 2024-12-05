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

export async function GetProductDetail({ commit}, productId){
    try {
        const response = await axiosClient.get(`Product/GetProductDetail?productId=${productId}`);
        commit('Set_ProductDetail', response.data);
    } catch (error) {
        console.log(error);
    }
}

export async function GetCart({ commit}, salesInvoiceId){
    try {
        const response = await axiosClient.get(`SaleInvoice/GetSaleInvoice?salesInvoiceId=${salesInvoiceId}`);
        commit('Set_Cart', response.data);
    } catch (error) {
        console.log(error);
    }
}

export async function GetCartDetail({ commit}, cartId){
    try {
        const response = await axiosClient.get(`SaleDetails/GetAllSaleDetail?saleInvoiceId=${cartId}`);
        commit('Set_CartDetail', response.data.$values);
    } catch (error) {
        console.log(error);
    }
}

