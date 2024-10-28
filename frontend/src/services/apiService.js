import axios from 'axios';

const API_URL = 'http://localhost:5223/cart';

export default {
  getCart() {
    return axios.get(`${API_URL}`);
  },
  addItemToCart(item) {
    return axios.post(`${API_URL}/add`, item);
  },
  updateItemQuantity(itemId, quantity) {
    return axios.patch(`${API_URL}/updateQuantity/${itemId}`, quantity, {
        headers: {
            'Content-Type': 'application/json'
        }
    });
},  
  removeItemFromCart(itemId) {
    return axios.delete(`${API_URL}/remove/${itemId}`);
  },
};
