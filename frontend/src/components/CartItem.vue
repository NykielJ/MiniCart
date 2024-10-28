<template>
  <div class="cart-item">
    <img class="cart-item__image" />
    <div class="cart-item__details">
      <h4 class="cart-item__name">{{ item.name }}</h4>
      <div class="cart-item__attributes">
        <span v-if="item.size">Size: {{ item.size }}</span>
        <span v-if="item.color">Color: {{ item.color }}</span>
        <span v-if="item.pattern">Pattern: {{ item.pattern }}</span>
      </div>
      <div class="cart-item__quantity-price">
        <div class="cart-item__quantity-control-box">
          <button 
            @click="decreaseQuantity" 
            :class="{'cart-item__qty-button--disabled': localQuantity === 1}" 
            :disabled="localQuantity === 1" 
            class="cart-item__qty-button cart-item__qty-button--left">
            -
          </button>
          <input type="text" v-model="localQuantity" readonly class="cart-item__qty-field" />
          <button @click="increaseQuantity" class="cart-item__qty-button cart-item__qty-button--right">+</button>
        </div>
        <div class="cart-item__price">{{ (item.price * localQuantity).toFixed(2) }} €</div>
      </div>
    </div>
    <button class="cart-item__remove-button" @click="removeItem">x</button>
  </div>
</template>

<script>
export default {
  props: {
    item: {
      type: Object,
      required: true,
    },
  },
  data() {
    return {
      localQuantity: this.item.quantity,
    };
  },
  watch: {
    'item.quantity'(newQuantity) {
      this.localQuantity = newQuantity;
    },
  },
  methods: {
    increaseQuantity() {
      this.$emit('increase-quantity', this.item);
    },
    decreaseQuantity() {
      if (this.item.quantity > 1) {
        this.$emit('decrease-quantity', this.item);
      }
    },
    removeItem() {
      this.$emit('remove-item', this.item);
    },
  },
};
</script>

<style scoped>
.cart-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 5px 0;
box-shadow: 0 0 5px 0 rgba(0, 0, 0, 0.1);
  padding: 5px;
  margin-bottom: 5px ;
  margin-top: 5px;
  border-radius: 5px;
  background-color: #ffffff; 
}

.cart-item__image {
  width: 100px;
  height: 75px;
  background-color: #e6e6e6;
  margin-right: 10px;
}

.cart-item__details {
  flex: 1;
  display: flex;
  flex-direction: column;
  margin-right: 10px;
}

.cart-item__name {
  font: bold 14px Arial;
  color: #565656; 
  margin-bottom: 5px;
}

.cart-item__attributes span {
  display: block;
  font: regular 14px Arial;
  color: #565656;
}

.cart-item__quantity-price {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: 5px;
}

.cart-item__quantity-control-box {
  display: flex;
  align-items: center;
  border: 1px solid #b0bec5;
  border-radius: 5px;
  background-color: #f5f5f5; 
}

.cart-item__qty-button {
  width: 25px;
  height: 30px;
  background-color: #ff4d4d; 
  border: none;
  color: white;
  cursor: pointer;
}

.cart-item__qty-button--left {
  border-top-left-radius: 4px;
  border-bottom-left-radius: 4px;
}

.cart-item__qty-button--right {
  border-top-right-radius: 4px;
  border-bottom-right-radius: 4px;
}

.cart-item__qty-button--disabled {
  background-color: #b0bec5; 
}

.cart-item__qty-field {
  width: 40px;
  height: 25px;
  text-align: center;
  border: none;
  background-color: white;
}

.cart-item__price {
  font: bold 14px Arial;
  color: #565656; 
  margin-left: 15px;
}

.cart-item__remove-button {
  background: none;
  border: none;
  color: #d32f2f;
  cursor: pointer;
  font-size: 20px;
}
</style>
