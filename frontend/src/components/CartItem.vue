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
        <div class="cart-item__price">
          {{ (item.price * localQuantity).toFixed(2) }} €
        </div>
      </div>
    </div>
    <div class="cart-item__quantity-control-box">
      <button @click="increaseQuantity" class="cart-item__qty-button">+</button>
      <input type="text" v-model="localQuantity" readonly class="cart-item__qty-field" />
      <button @click="decreaseQuantity" class="cart-item__qty-button">-</button>
    </div>
    <ConfirmationDialog
      ref="confirmationDialog"
      message="Do you want to remove this item from the cart?"
      @confirm="confirmRemoval"
      @cancel="cancelRemoval"
    />
  </div>
</template>

<script>
import ConfirmationDialog from "./ConfirmationDialog.vue";

export default {
  components: {
    ConfirmationDialog,
  },
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
    "item.quantity"(newQuantity) {
      this.localQuantity = newQuantity;
    },
  },
  methods: {
    increaseQuantity() {
      this.$emit("increase-quantity", this.item);
    },
    decreaseQuantity() {
      if (this.localQuantity === 1) {
        // Wywołanie metody showDialog w ConfirmationDialog
        this.$refs.confirmationDialog.showDialog();
      } else {
        this.$emit("decrease-quantity", this.item);
      }
    },
    confirmRemoval() {
      this.$emit("remove-item", this.item);
    },
    cancelRemoval() {
      // Brak akcji - użytkownik anulował usunięcie
    },
  },
};
</script>

<style scoped>
.cart-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 5px 10px 10px 5px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1), 0 0 10px rgba(0, 0, 0, 0.05);
  margin-bottom: 10px;
  margin-top: 10px;
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
  margin-bottom: auto;
  margin-top: auto;
}

.cart-item__attributes span {
  display: block;
  font: 14px Arial;
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
  flex-direction: column;
  align-items: center;
  border-radius: 5px;
  background-color: #F5F4F9;
  overflow: hidden;
}

.cart-item__qty-button {
  width: 30px;
  height: 30px;
  border: none;
  color: #C48793;
  cursor: pointer;
  background-color: #F5F4F9;
}

.cart-item__qty-button:first-child {
  border-top-left-radius: 5px;
  border-top-right-radius: 5px;
}

.cart-item__qty-button:last-child {
  border-bottom-left-radius: 5px;
  border-bottom-right-radius: 5px;
}

.cart-item__qty-field {
  width: 25px;
  height: 20px;
  margin-top: 5px;
  margin-bottom: 5px;
  text-align: center;
  outline: none;
  border: none;
  background-color: #F5F4F9;
}

.cart-item__price {
  font: bold 14px Arial;
  color: #565656;
  margin-left: 15px;
}
</style>
