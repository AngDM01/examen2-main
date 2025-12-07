<template>
  <div class="pop-up">
    <div class="pop-up-inner">
      <div class="pop-up-close" @click="closePopUp">&times;</div>

      <div class="pay-info">
        <h2>Total a pagar: ₡{{ totalPrice }}</h2>
      </div>

      <div class="pay-selection">
        <h3>Billetes:</h3>
        <div class="row">
          <div class="bill" v-for="bill in bills" :key="bill" @click="addBill(bill)">
            <p>₡{{ bill }}</p>
          </div>
        </div>

        <h3>Monedas:</h3>
        <div class="row">
          <div class="coin" v-for="coin in coins" :key="coin" @click="addCoin(coin)">
             <p>₡{{ coin }}</p>
          </div>
        </div>
      </div>

      <p>A pagar: {{ payment.totalAmount }}</p>

      <CoffeeRegularButton text="Pagar" @click="PayOrder" />
    </div>
  </div>
</template>

<script setup>
import { reactive, defineProps, defineEmits} from 'vue'

import CoffeeRegularButton from '../CoffeeRegularButton.vue'

const props = defineProps({
  totalPrice: { type: Number, required: true },
})

const emit = defineEmits(['close', 'confirm-payment']);

const payment = reactive({
  totalAmount: 0,
  bills: [],
  coins: []
})

const bills = [1000]
const coins = [500, 100, 50, 25]

function addBill(amount) {
  payment.bills.push(amount)
  payment.totalAmount += amount
}


function addCoin(amount) {
  payment.coins.push(amount)
  payment.totalAmount += amount
}

async function PayOrder() {
  if (payment.totalAmount < props.totalPrice) {
    alert("El pago no cubre el total");
    return;
  }

  emit('confirm-payment', {
    ...payment
  });

  emit('close');
}

</script>

<style scoped>
.pop-up {
  position: fixed;
  top:0;
  left:0;
  width:100%;
  height:100%;
  background: rgba(0,0,0,0.5);
  display:flex;
  justify-content:center;
  align-items:center;
  z-index:1000;
}

.pop-up-inner {
  background: #fff;
  padding: 20px 30px;
  border-radius: 15px;
  width: 400px;
  max-width: 90%;
  position: relative;
}

.pop-up-close {
  position:absolute;
  top:10px;
  right:15px;
  font-size:25px;
  cursor:pointer;
}

.row {
  display:flex;
  gap:15px;
  flex-wrap:wrap;
  margin-top:10px;
}

.bill, .coin {
  background: #f5f5f5;
  border: 1px solid #ccc;
  border-radius: 10px;
  padding: 15px 20px;
  font-weight:bold;
  text-align:center;
  min-width:60px;
  cursor:pointer;
  user-select:none;
  transition:0.2s;
}

.bill:hover, .coin:hover {
  background:#e0e0e0;
}
</style>
