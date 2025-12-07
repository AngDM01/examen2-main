<template>
  <div class="catalog">
    <h2>CATÁLOGO</h2>
    <div class="coffees-list ">
      <div v-for="coffee in coffeesData" :key="coffee.name">
        <CoffeeCard
          :coffeeName="coffee.coffeeName"
          :coffeePrice="coffee.coffeePrice"
          :CoffeeAvaibleUnities="coffee.coffeeStock"
          @add-to-order="AddCoffeesToOrder"
        />
      </div>
    </div>
  </div>

  <PayPopUp 
    v-if="payPopUp" 
    :totalPrice="totalPrice" 
    @close="togglePayPopUp" 
    @confirm-payment="handlePayment" 
  />


  <div class="order">
    <h2>PEDIDO</h2>
    <div class="order-list" v-if="orderCoffeeList.length > 0">
      <div class="order-table-container">
        <table class="order-table">
          <thead>
            <tr>
              <th>Café</th>
              <th>Cantidad</th>
              <th>Precio por unidad</th>
              <th>Costo total</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="coffee in orderCoffeeList" :key="coffee.coffeeName">
              <td><p>{{ coffee.coffeeName }}</p></td>
              <td><p>{{ coffee.quantity }}</p></td>
              <td><p>₡{{ coffee.coffeePrice }}</p></td>
              <td><p>₡{{ coffee.coffeePrice * coffee.quantity }}</p></td>
              <td><CoffeeCircleButton msg="-" @click="removeUnityFromOrder(coffee.coffeeName)"/></td>
            </tr>
          </tbody>
          <tfoot>
            <tr>
              <td colspan="3"><p>Total</p></td>
              <td><p>₡{{ totalPrice }}</p></td>
            </tr>
          </tfoot>
        </table>
      </div>
      
      <CoffeeRegularButton text="Continuar el Pago" @click="BuyCoffee()" />
    </div>
    <div v-else>
      <p>No se ha añadido un producto aún.</p>
    </div>
  </div>
</template>

<script setup>
import { onMounted, ref, computed } from 'vue';

import URLBase from '../AxiosGlobalApi.js';

import CoffeeCard from './CoffeeCard.vue';
import CoffeeRegularButton from './CoffeeRegularButton.vue';
import CoffeeCircleButton from './CoffeeCircleButton.vue';
import PayPopUp from './popUps/PayPopUp.vue';

const coffeesData = ref([]);

const orderCoffeeList = ref([]);

const payPopUp = ref(false);

const order = ref({});

const totalPrice = computed(() => {
  return orderCoffeeList.value.reduce((sum, c) => sum + c.coffeePrice * c.quantity, 0);
});

async function GetCoffeesData() {
  var coffeesStock = await GetCoffeesStock();

  var coffeesPrices = await GetCoffeesPrices();

  coffeesData.value = coffeesStock.map(coffee => {
    const coffeeprice = coffeesPrices.find(p => p.coffeeName === coffee.coffeeName);
    return {
      coffeeName: coffee.coffeeName,
      coffeePrice: coffeeprice ? coffeeprice.coffeePrice : 0,
      coffeeStock: coffee.coffeeStock
    };
  });
}

async function GetCoffeesStock() {
  try {
    const serverResponse = await URLBase.get("/Api/Coffee/Stocks");

    return serverResponse.data;
  } catch (ex) {
    console.error("Error al obtener el precio de los productos:", ex);
    return [];
  }
}

async function GetCoffeesPrices() {
  try {
    const serverResponse = await URLBase.get("/Api/Coffee/Prices");

    return serverResponse.data;
  } catch (ex) {
    console.error("Error al obtener datos:", ex);
  }
}

function AddCoffeesToOrder(coffeeData) {
  const coffeeAlreadyIn = orderCoffeeList.value
      .find(c => c.coffeeName === coffeeData.coffeeName);

  if (coffeeAlreadyIn) {
    coffeeAlreadyIn.quantity += coffeeData.quantity || 0;
  } else {
    orderCoffeeList.value.push({
      ...coffeeData,
      quantity: coffeeData.quantity || 0
    });
  }
}

function removeUnityFromOrder(coffeeName) {
  const coffeeInOrder = orderCoffeeList.value
      .find(c => c.coffeeName === coffeeName);

  if (coffeeInOrder) {
    coffeeInOrder.quantity -= 1;
    if (coffeeInOrder.quantity < 1) {
      const index = orderCoffeeList.value.findIndex(c => c.coffeeName === coffeeName);
      if (index !== -1) {
        orderCoffeeList.value.splice(index, 1);
      }
    }
  }
}

async function BuyCoffee() {
  order.value = buildOrder();

  togglePayPopUp();
}

function buildOrder() {
  const order = {};

  orderCoffeeList.value.forEach(coffee => {
    order[coffee.coffeeName] = coffee.quantity;
  });

  return order;
}

function togglePayPopUp() {
  payPopUp.value = !payPopUp.value;
}

async function handlePayment(payment) {
  const orderValue = order.value;

  try {
    const request = { order: orderValue, payment: payment };

    const response = await URLBase.post("/Api/Coffee/Buy", request);

    const data = await response.data;

    alert(`Compra realizada con éxito!\nCambio total: ₡${data.TotalChange}`);

    console.log("Desglose del cambio:");
    data.ChangeBreakdown.forEach(item => {
      console.log(`₡${item.Denomination}: ${item.Quantity} unidades`);
    });

    orderCoffeeList.value = [];
    order.value = {};

    window.location.reload();
  } catch (er) {
    alert("Error al procesar la compra:", er);
  }
}

onMounted(async () => {
  await GetCoffeesData();
})
</script>

<style scoped>
.catalog {
  width: 100%;
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.coffees-list {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
  padding: 20px;

  border: 1px solid #B08968;
  border-radius: 10px;
}

.coffees-list > div {
  flex: 1 1 220px;
  display: flex;
  justify-content: center;
}

.order {
  width: 100%;
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.order-list {
  width: 50%;
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.order-table-container {
  width: 100%;
  overflow-x: auto;
  padding: 20px;

  border: 1px solid #B08968;
  border-radius: 10px;
}

.order-table {
  width: 100%;
  border-collapse: collapse;
  background-color: none;
  overflow: hidden;
}

.order-table th,
.order-table td{
  padding-right: 10px;
  text-align: left;
  font-family: "Quicksand", sans-serif;
  font-size: 16px;
  font-weight: 400;
  line-height: 30px;
  letter-spacing: 0.05em;
  color: #6D8B74;
  margin: 0;
}

.order-table thead {
  color: #6D8B74;
}

.order-table tbody tr {
  transition: background 0.2s;
}

.order-table tbody tr:hover {
  background-color: rgba(176,137,104,0.1);
}

</style>
