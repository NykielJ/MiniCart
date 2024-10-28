<template>
  <div id="app">
    <div class="container">
        <div class="col-12 col-md-6">
          <ShoppingCart />
        </div>
    </div>
  </div>
</template>

<script>
import ShoppingCart from './components/ShoppingCart.vue';

export default {
  components: {
    ShoppingCart,
  },
};
</script>

<style>
body {
  margin: 0;
  font-family: Arial, sans-serif;
}

.container {
  display: flex;
  justify-content: center;
  min-height: 100vh;
  background-color: #ff4d4d;
}

.col-12 {
  flex: 0 0 100%;
}

.col-md-6 {
  flex: 0 0 50%;
}
</style>