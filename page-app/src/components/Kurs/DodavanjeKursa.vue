<template>
  <div class="formdiv">
    <form>
      <div class="form-group">
        <label>Naziv Kursa </label>
        <validation-provider
          v-model="Ime"
          rules="required|min:3"
          v-slot="{ errors }"
        >
          <input
            v-model="Ime"
            type="text"
            class="form-control"
            placeholder="Kurs"
          />
          <li style="color:red" v-for="(erorr, ind) in errors" :key="ind">
            <span>{{ errors[ind] }}</span>
          </li>
        </validation-provider>
      </div>
      <div class="checkboxDiv" v-for="(stud, ind) in students" :key="ind">
        <label
          >{{ stud.Ime }} {{ stud.Prezime }} {{ stud.Brojindeksa }}
          <input
            class="checkbox"
            type="checkbox"
            v-bind:id="stud.Studenid"
            v-bind:value="stud.Studentid"
            v-model="checkedNames"
          />
        </label>
        <br />
      </div>
    </form>
    <div class="modal-body p-4">
      <button @click="provjeriValidnost()" class="btn btn-info">
        Kreiraj kurs
      </button>
    </div>

    <!-- uspjesno dijalog -->

    <div id="overlay" v-if="showInfoDialog">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              Uspjesno ste izvrsili kreiranje kursa {{ Ime }}
            </h5>
          </div>
          <div class="modal-body p-4">
            <button @click="potvrda()" class="btn btn-success">
              Zatvori
            </button>
          </div>
        </div>
      </div>
    </div>

    <!-- Greska dijalog -->
    <div id="overlay" v-if="showErrorDialog">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">{{ poruka }}</h5>
            <button
              type="button"
              class="close"
              @click="showErrorDialog = false"
            >
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body p-4">
            <button class="btn btn-danger" @click="showErrorDialog = false">
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
import { ValidationProvider, extend } from "vee-validate";
import { required, min } from "vee-validate/dist/rules";

//import { required, minLength } from "vuelidate/lib/validators";
import Vue from "vue";

extend("required", {
  ...required,
  message: "Ovo polje je obavezno!",
});
extend("min", {
  ...min,
  message: "Ovo polje mora da sadrži minimalno 3 slova!",
});

export default {
  components: {
    ValidationProvider,
  },
  data() {
    return {
      checkedNames: [],
      students: [],
      kursevi: [],
      Ime: "",
      showErrorDialog: false,
      showInfoDialog: false,
      poruka: "",
    };
  },

  created() {
    if (Vue.prototype.$ulogovan) {
      this.getKursevi();
      this.allStudents();
    } //else this.$router.push("/Login");
  },
  methods: {
    allStudents() {
      axios
        .get(Vue.prototype.$adresa + "getStudent", {
          headers: {
            Authorization: Vue.prototype.$pristupniToken,
          },
        })
        .then((res) => {
          this.students = res.data;
        });
    },
    getKursevi() {
      axios
        .get(Vue.prototype.$adresa + "getKursevi", {
          headers: {
            Authorization: Vue.prototype.$pristupniToken,
          },
        })
        .then((res) => {
          this.kursevi = res.data;
        });
    },
    kreirajKurs() {
      let con = Vue.prototype.$adresa + "kreirajKurs";
      axios
        .get(con, {
          params: { Ime: this.Ime, niz: this.checkedNames },
          headers: {
            Authorization: Vue.prototype.$pristupniToken,
          },
        })
        .then(this.$router.push("/Kursevi"));
    },
    potvrda() {
      this.showInfoDialog = false;
      this.kreirajKurs();
    },
    provjeriValidnost() {
      let pomocna = true;
      for (let i = 0; i < this.kursevi.length; i++) {
        if (
          this.Ime == this.kursevi[i].Nazivkursa ||
          this.Ime.length < 2 ||
          this.checkedNames.length < 1
        ) {
          if (this.checkedNames.length < 1)
            this.poruka =
              "Izaberite minimum jednog studenta pri kreiranju kursa!";
          else if (this.Ime == this.kursevi[i].Nazivkursa)
            this.poruka = "Kurs pod navedenim imenom vec postoji!";
          this.showErrorDialog = true;
          pomocna = false;
        }
      }
      if (pomocna) {
        this.showInfoDialog = true;
      }
    },
  },
};
</script>
<style scoped>
form {
  background-color: azure;
  margin: auto;
  width: 50%;
  height: 80%;
  border: 1px solid black;
  border-radius: 5%;
}
input {
  margin: auto;
  width: 70%;
}
button {
  width: 35%;
  font-weight: bold;
}
label {
  font-weight: bold;
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
.checkbox {
  margin-left: auto;
}
</style>
