<template>
  <div class="shopping-cart">
    <h2 class="shopping-cart__title">My Cart ({{ cartItems.length }})</h2>
    <div class="shopping-cart__items">
      <CartItem
        v-for="item in cartItems"
        :key="item.id"
        :item="item"
        @increase-quantity="increaseQuantity"
        @decrease-quantity="decreaseQuantity"
        @remove-item="removeItem"
      />
    </div>
    <div class="shopping-cart__summary">
      <p class="shopping-cart__shipping-cost">Shipping: {{ shippingCost.toFixed(2) }} €</p>
      <h3 class="shopping-cart__order-total">Order Total: {{ orderTotal }} €</h3>
      <div>
      <button class="shopping-cart__checkout-button">Checkout</button>
    </div>
    </div>
  </div>
</template>

<script>
import CartItem from './CartItem.vue';
import apiService from '../services/apiService';

export default {
  components: {
    CartItem,
  },
  data() {
    return {
      cartItems: [],
      shippingCost: 15.0,
    };
  },
  computed: {
    orderTotal() {
      return (
        this.cartItems.reduce((total, item) => total + (item.price * item.quantity), 0) +
        this.shippingCost
      ).toFixed(2);
    },
  },
  methods: {
    async increaseQuantity(item) {
      const updatedQuantity = item.quantity + 1;
      try {
        const response = await apiService.updateItemQuantity(item.id, updatedQuantity);
        this.cartItems = response.data.items;
      } catch (error) {
        console.error('Błąd przy zwiększaniu ilości:', error);
      }
    },

    async decreaseQuantity(item) {
      if (item.quantity > 1) {
        const updatedQuantity = item.quantity - 1;
        try {
          const response = await apiService.updateItemQuantity(item.id, updatedQuantity);
          this.cartItems = response.data.items;
        } catch (error) {
          console.error('Błąd przy zmniejszaniu ilości:', error);
        }
      }
    },

    async removeItem(item) {
      try {
        const response = await apiService.removeItemFromCart(item.id);
        this.cartItems = response.data.items;
      } catch (error) {
        console.error('Błąd przy usuwaniu przedmiotu:', error);
      }
    },
  },

  mounted() {
    apiService.getCart()
      .then(response => {
        this.cartItems = response.data.items;
        this
      })
      .catch(error => {
        console.error('Błąd przy ładowaniu koszyka:', error);
      });
  },
};
</script>

<style scoped>
.shopping-cart {
  display: flex;
  flex-direction: column;
  height: 100vh;
  max-width: 520px;
  width: 36%;
  min-width: 460px;
  margin-left: auto;
  background-color: #F5F4F9;
  border: 1px solid #ff0000;
  border-radius: 8px;
  padding: 20px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  box-sizing: border-box;
  position: fixed;
  top: 0;
  right: 0;
  overflow-y: auto;
}

.shopping-cart__title {
  display: flex;
  justify-content: center;
  align-items: center;
  font: bold 24px Arial, sans-serif;
  color: #000000;
  margin-top: auto;
  margin-bottom:  auto;
}


.shopping-cart__items {
  max-height: 100vh;
  overflow-y: auto;
  margin-bottom: auto;
  margin-right: -15px;
  padding-right: 10px;
}

.shopping-cart__items::-webkit-scrollbar {
  width: 8px;
}

.shopping-cart__items::-webkit-scrollbar-track {
  background: #ffe6e6;
  border-radius: 4px;
}

.shopping-cart__items::-webkit-scrollbar-thumb {
  background-color: #ff6666;
  border-radius: 4px;
  border: 2px solid #ffe6e6;
}

.shopping-cart__items::-webkit-scrollbar-thumb:hover {
  background-color: #ff4d4d;
}

.shopping-cart__summary {
  border-top: 1px solid #E5E4EA;
  background-color: #F5F4F9;
  padding: 0 20px;
  margin-top: auto;
  display: flex;
  flex-direction: column;
}

.shopping-cart__shipping-cost {
  font: bold 16px Arial, sans-serif;
  color: #575757;
}

.shopping-cart__order-total {
  margin: 10px 0;
  font: bold 18px Arial, sans-serif;
  color: #000000;
}

.shopping-cart__checkout-button {
  background-color: #ff4d4d;
  color: #ffffff;
  border: none;
  padding: 15px;
  width: 100%;
  cursor: pointer;
  border-radius: 8px;
  font: bold 16px Arial, sans-serif;
  margin-top: 20px;
  transition: background-color 0.3s ease;
}

.shopping-cart__checkout-button:hover {
  background-color: #cc0000;
}

.shopping-cart__checkout-button:active {
  background-color: #990000;
}

@media (max-width: 768px) {
  .shopping-cart {
    width: 100%;
    height: 100vh;
    margin: 0;
    padding: 1rem;
    min-width: 100%;
    border: none;
    border-radius: 0;
    left: 0;
    right: 0;
    position: fixed;
    overflow-x: hidden;
  }

  .shopping-cart__title {
    font-size: 1.5rem;
  }

  .shopping-cart__shipping-cost,
  .shopping-cart__order-total {
    font-size: 1.25rem;
  }

  .shopping-cart__checkout-button {
    font-size: 1rem;
  }
  
  .shopping-cart__summary {
    
    position: sticky;
    bottom: 0;
    background-color: #F5F4F9;
    padding: 15px 20px;
  }
}
</style>

