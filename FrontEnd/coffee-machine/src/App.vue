<template>
  <CoffeeHeader />
  <div class="content">
    <CoffeeHome />
  </div>
  <CoffeeFooter />
</template>

<script>
import CoffeeHeader from './components/CoffeeHeader.vue';
import CoffeeHome from './components/CoffeeHome.vue'
import CoffeeFooter from './components/CoffeeFooter.vue';

export default {
  name: 'App',
  components: {
    CoffeeHeader,
    CoffeeHome,
    CoffeeFooter
  }
}
</script>

<style>
#app {
  background-color: #FAF7F2;
  display: flex;
  flex-direction: column;
  gap: 50px;
  padding: 0;
  margin: 0;
  height: 100%;
  width: 100%;
}

#app h1 {
  font-family: "Julius Sans One", sans-serif;
  font-size: 32px;
  font-weight: 400;
  line-height: 40px;
  letter-spacing: 0.05em;
  color: #6D8B74;
  margin: 0;
}

#app h2 {
  font-family: "Julius Sans One", sans-serif;
  font-size: 24px;
  font-weight: 400;
  line-height: 40px;
  letter-spacing: 0.05em;
  color: #1B1A17;
  margin: 0;
}
#app h3 {
  font-family: "Quicksand", sans-serif;
  font-size: 20px;
  font-weight: 400;
  line-height: 30px;
  letter-spacing: 0.05em;
  color: #1B1A17;
  margin: 0;
}

#app p {
  font-family: "Quicksand", sans-serif;
  font-size: 16px;
  font-weight: 400;
  line-height: 30px;
  letter-spacing: 0.05em;
  color: #1B1A17;
  margin: 0;
}

.content {
  display: flex;
  flex-direction: column;
  gap: 50px;
  padding: 0 50px;
  height: 100%;
}
</style>
