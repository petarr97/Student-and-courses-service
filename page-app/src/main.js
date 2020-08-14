import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import Vuelidate from "vuelidate";

Vue.use(Vuelidate);
Vue.config.productionTip = false;

Vue.prototype.$adresa = "https://localhost:44349/";

Vue.prototype.$pristupniToken = "";
Vue.prototype.$ulogovan = false;
Vue.prototype.$registracija = false;

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount("#app");
