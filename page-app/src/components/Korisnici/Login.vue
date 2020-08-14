<template>
  <div>
    <form @submit.prevent="prijaviKorisnika()">
      <div class="form-group">
        <label for="Username">Username </label>
        <input
          type="text"
          class="form-control"
          placeholder="Username"
          v-model="user.username"
        />
      </div>
      <div class="form-group">
        <label for="Password">Password</label>
        <input
          type="password"
          class="form-control"
          placeholder="Password"
          v-model="user.password"
        />
      </div>

      <button type="submit" class="btn btn-primary">Potvrdi</button>
      <button @click="registrujKorisnika()" class="btn btn-info">
        Registracija
      </button>
    </form>
    <div id="overlay" v-if="showErrorDialog">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">{{ poruka }}</h5>
          </div>
          <div class="modal-body p-4">
            <button
              type="submit"
              class="btn btn-danger"
              @click="showErrorDialog = false"
            >
              Zatvori
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Vue from "vue";

export default {
  data() {
    return {
      user: {
        username: "",
        password: "",
      },
      showErrorDialog: false,
      poruka: "",
    };
  },
  methods: {
    prijaviKorisnika() {
      this.getToken();
    },
    provjeriLogovanje(provjera) {
      if (provjera == true) {
        Vue.prototype.$ulogovan = true;
        this.$router.push("/");
      } else {
        this.poruka = "Pogresni podaci za logovanje!!";
        this.showErrorDialog = true;
      }
    },
    getToken() {
      var params = new URLSearchParams();
      params.append("grant_type", "password");
      params.append("password", this.user.password);
      params.append("username", this.user.username);
      axios
        .post(Vue.prototype.$adresa + "token", params, {
          headers: {
            "content-type": "application/x-www-form-urlencoded",
          },
        })
        .then(
          (res) => {
            (Vue.prototype.$pristupniToken =
              res.data.token_type + " " + res.data.access_token),
              this.ulogujSe();
          },
          (error) => (
            ((this.poruka = "Pogresni podaci za logovanje!"),
            (this.showErrorDialog = true)),
            console.log(error)
          )
        );
    },
    registrujKorisnika() {
      Vue.prototype.$registracija = true;
      this.$router.push("/Registracija");
    },
    ulogujSe() {
      axios
        .post(Vue.prototype.$adresa + "api/data/getForAdmin", this.user, {
          headers: {
            Authorization: Vue.prototype.$pristupniToken,
          },
        })
        .then((res) => this.provjeriLogovanje(res.data));
    },
  },
};
</script>

<style scoped>
label {
  font-weight: bold;
}
input {
  margin: auto;
  width: 50%;
  margin-bottom: 2ch;
}
form {
  background-color: azure;
  margin: auto;
  width: 30%;
  height: 80%;
  border: 1px solid black;
  border-radius: 5%;
  margin-top: 4ch;
}
button {
  margin-bottom: 2ch;
  margin-right: 2ch;
}
#overlay {
  position: fixed;
  display: block;
  height: 100%;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.5);
  z-index: 2;
  cursor: pointer;
}
</style>
