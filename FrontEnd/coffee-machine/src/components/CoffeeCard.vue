<template>
  <div class="coffee-card">
    <h3>{{ coffeeName }}</h3>
    <component :is="selectedCoffee.component"></component>
    <p>₡{{ coffeePrice }}</p>
    <p>Unidades disponibles: {{ CoffeeAvaibleUnities }}</p>
    <div class="quantity-container">
      <div class="quantity-selector">
        <CoffeeCircleButton msg="-" @click="decreaseQuantity()"/>
        <p>{{ quantity }}</p>
        <CoffeeCircleButton @click="increaseQuantity()"/>
      </div>
      <CoffeeRegularButton text="Añadir"  @click="addCoffee"/>
    </div>
  </div>
</template>

<script setup>
import { computed, defineEmits, ref, defineProps } from "vue";

import CoffeeCircleButton from './CoffeeCircleButton.vue';
import CoffeeRegularButton from './CoffeeRegularButton.vue';
import CoffeeAmericanoImg from "./coffeesImages/CoffeeAmericanoImg.vue";
import CoffeeLateImg from "./coffeesImages/CoffeeLateImg.vue";
import CoffeeCappuccinoImg from "./coffeesImages/CoffeeCappuccinoImg.vue";
import CoffeeMocaccinoImg from "./coffeesImages/CoffeeMocaccinoImg.vue";

const props = defineProps({
  coffeeName: { type: String, default: "cafe" },
  coffeePrice: { type: Number, default: 0 },
  CoffeeAvaibleUnities: { type: Number, default: 0 }
});

const quantity = ref(0);

const coffeeComponents = {
  americano: CoffeeAmericanoImg,
  late: CoffeeLateImg,
  cappuccino: CoffeeCappuccinoImg,
  mocaccino: CoffeeMocaccinoImg
};

const selectedCoffee = computed(() => {
  const key = props.coffeeName
    .toLowerCase()
    .normalize("NFD").replace(/[\u0300-\u036f]/g, "")
    .replace(/\s+/g, "");

  return {
    component: coffeeComponents[key] || CoffeeAmericanoImg
  };
});

function decreaseQuantity() {
  if (quantity.value === 0)
    return;

  quantity.value -= 1;
}

function increaseQuantity() {
  if (quantity.value < props.CoffeeAvaibleUnities)
    quantity.value += 1;
  
  return;
}

const emit = defineEmits(["add-to-order"]);

function addCoffee() {
  if (quantity.value === 0)
    return;

  return emit("add-to-order", {
    coffeeName: props.coffeeName,
    coffeePrice: props.coffeePrice,
    available: props.CoffeeAvaibleUnities,
    quantity: quantity.value
  });
}
</script>

<style scoped>
.coffee-card {
  display: flex;
  flex-direction: column;
  padding: 20px;
  border: 1px solid rgba(176, 137, 104, 0.5);
  border-radius: 10px;
}

.quantity-container {
  display: flex;
  justify-content: space-between;
  gap: 20px;
}

.quantity-selector {
  display: flex;
  gap: 20px;
}

.quantity-selector p {
  display: flex;
  justify-content: center;
  align-items: center;
  text-align: center;
}

.txt-inp {
  background: none;
  border: none;
  width: auto;
}
</style>