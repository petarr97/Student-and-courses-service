<template>
  <div id="formdiv">
    <form @submit.prevent="registrujKorisnika()">
      <div class="form-group">
        <label for="Username">Unesite username: </label>
        <input
          type="text"
          class="form-control"
          placeholder="Username"
          v-model="user.username"
        />
      </div>
      <div class="form-group">
        <label for="Password">Unesite password:</label>
        <input
          type="password"
          class="form-control"
          placeholder="Password"
          v-model="user.password"
        />
      </div>
      <button type="submit" class="btn btn-primary">Registruj</button>
      <button @click="logIn()" type="submit" class="btn btn-primary">
        Log In
      </button>
    </form>

    <div id="overlay" v-if="showInfoDialog">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              {{ poruka }}
            </h5>
            <button type="button" class="close" @click="showInfoDialog = false">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body p-4">
            <button
              type="submit"
              class="btn btn-success"
              @click="(showInfoDialog = false), logIn()"
            >
              Zatvori
            </button>
          </div>
        </div>
      </div>
    </div>

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
import Vue from "vue";
import axios from "axios";

export default {
  data() {
    return {
      user: {
        username: "",
        password: "",
      },
      poruka: "",
      showErrorDialog: false,
      showInfoDialog: false,
    };
  },
  methods: {
    logIn() {
      Vue.prototype.$registracija = false;
      this.$router.push("/Login");
    },
    registrujKorisnika() {
      if (this.user.username.length > 3 && this.user.username.length > 3) {
        axios
          .post(
            Vue.prototype.$adresa + "api/data/registrujKorisnika",
            this.user
          )
          .then((res) => this.provjeriRegistraciju(res.data));
      } else {
        this.poruka =
          "Username i password moraju sadrzati vise od 3 karaktera!";
        this.showErrorDialog = true;
      }
    },
    provjeriRegistraciju(provjera) {
      if (provjera == true) {
        this.poruka = "Uspjesna registracija";
        this.showInfoDialog = true;
      } else {
        this.poruka = provjera;
        this.showErrorDialog = true;
      }
    },
  },
};
</script>

<style scoped>
label {
  font-weight: bold;
  color: white;
}
input {
  margin: auto;
  width: 50%;
  margin-bottom: 2ch;
}
form {
  background-color: dimgray;
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
