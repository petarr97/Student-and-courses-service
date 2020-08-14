<template>
  <div id="app">
    <link
      rel="stylesheet"
      href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"
      integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh"
      crossorigin="anonymous"
    />
    <div id="nav" v-if="showNavBar">
      <router-link to="/Korisnici">PrikazStudenata</router-link> |
      <router-link to="/UnosKorisnika/0">Dodaj Studenta</router-link> |
      <router-link to="/Kursevi">Kursevi</router-link> |
      <router-link to="/dodavanjeKursa">Kreiraj Kurs</router-link>
      <button @click="LogOut()" v-if="showButton" class="btn btn-danger">
        Log Out
      </button>
    </div>
    <router-view />
  </div>
</template>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
<style scoped>
button {
  float: right;
}
</style>
<script>
import Vue from "vue";
export default {
  data() {
    return {
      showButton: true,
      showNavBar: false,
    };
  },
  created() {
    if (Vue.prototype.$ulogovan != true) {
      this.showNavBar = false;
      if (Vue.prototype.$registracija == false) this.$router.push("/Login");
    } else {
      this.showNavBar = true;
    }
  },
  updated() {
    if (Vue.prototype.$ulogovan != true) {
      this.showNavBar = false;
      if (Vue.prototype.$registracija == false) this.$router.push("/Login");
    } else {
      this.showNavBar = true;
    }
  },
  methods: {
    LogOut() {
      this.showNavBar = false;
      Vue.prototype.$ulogovan = false;
      this.$router.push("/Login");
    },
  },
};
</script>
