<template>
  <div>
    <form @submit.prevent="checkForm()">
      <div class="form-group">
        <label for="Ime">Ime </label>
        <validation-provider
          v-model="student.Ime"
          rules="required|min:3|samoSlova"
          v-slot="{ errors }"
        >
          <input
            type="text"
            class="form-control"
            placeholder="Unesite ime studenta"
            v-model="student.Ime"
          />
          <li style="color:red" v-for="(erorr, ind) in errors" :key="ind">
            <span>{{ errors[ind] }}</span>
          </li>
        </validation-provider>
      </div>
      <div class="form-group">
        <validation-provider
          v-model="student.Prezime"
          rules="required|min:3|samoSlova"
          v-slot="{ errors }"
        >
          <label for="Prezime">Prezime</label>
          <input
            type="text"
            class="form-control"
            id="Prezime"
            placeholder="Unesite prezime studenta"
            v-model="student.Prezime"
          />
          <li style="color:red" v-for="(erorr, ind) in errors" :key="ind">
            <span>{{ errors[ind] }}</span>
          </li>
        </validation-provider>
      </div>
      <div class="form-group">
        <validation-provider
          v-model="student.Brojindeksa"
          rules="between"
          v-slot="{ errors }"
        >
          <label for="BrojIndeksa">Broj Indeksa</label>
          <input
            type="text"
            class="form-control"
            id="BrIndeksa"
            name="brInd"
            placeholder="Unesite broj indeksa"
            v-model="student.Brojindeksa"
          />
          <li style="color:red" v-for="(erorr, ind) in errors" :key="ind">
            <span>{{ errors[ind] }}</span>
          </li>
        </validation-provider>
      </div>

      <div>
        <label>Izaberite Godinu Studija</label><br />
        <select id="cars" v-model="student.Godina">
          <option value="1">1</option>
          <option value="2">2</option>
          <option value="3">3</option>
          <option value="4">4</option>
        </select>
      </div>
      <div>
        <label>Izaberite status</label><br />
        <select id="status" v-model="student.Statusstudentaid">
          <option value="1">Redovan</option>
          <option value="2">Vanredan</option>
        </select>
      </div>
      <button type="submit" class="btn btn-primary">Potvrdi</button>
    </form>

    <div id="overlay" v-if="showInfoDialog">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              Uspjesno ste izvrsili dodavanje studenta {{ student.Ime }}
              {{ student.Prezime }}
            </h5>
            <button type="button" class="close" @click="showInfoDialog = false">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body p-4">
            <button
              type="submit"
              class="btn btn-success"
              @click="(showInfoDialog = false), addStudent()"
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
            <span aria-hidden="true">&times;</span>
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
<style scoped>
label {
  font-size: 2.6ch;
  font-weight: bold;
}
input {
  margin: auto;
  width: 50%;
}
form {
  background-color: azure;
  margin: auto;
  width: 40%;
  height: 80%;
  border: 1px solid black;
  border-radius: 5%;
}
button {
  width: 50%;
  margin-top: 10px;
  margin-bottom: 10px;
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

<script>
import axios from "axios";
import Vue from "vue";
import { ValidationProvider, extend } from "vee-validate";
import { required, min } from "vee-validate/dist/rules";

extend("required", {
  ...required,
  message: "Ovo polje je obavezno!",
});
extend("min", {
  ...min,
  message: "Ovo polje mora da sadrži minimalno 3 slova!",
});
var errorMessage = "Broj indeksa mora da sadrži 4 cifre";
extend("between", {
  message: errorMessage,
  validate: (value) => {
    if (value > 999 && value < 10000) {
      return true;
    }
    return false;
  },
});
var greska = "Ovo polje mora da sadrži samo slova!";
extend("samoSlova", {
  message: greska,
  validate: (value) => {
    if (/^[a-žA-Ž]+$/.test(value)) return true;
    return false;
  },
});

export default {
  components: {
    ValidationProvider,
  },
  data() {
    return {
      student: {
        Ime: "",
        Prezime: "",
        StudentId: 0,
        Brojindeksa: "",
        Godina: 1,
        Statusstudentaid: 1,
      },
      StudentId: "",
      BrojIndeksa: "",
      Ime: "",
      Prezime: "",
      Godina: 1,
      Status: 1,
      showInfoDialog: false,
      submitstatus: false,
      showErrorDialog: false,
      poruka: "",
      errorMessage: "",
    };
  },

  mounted: function() {
    if (Vue.prototype.$ulogovan) {
      this.checkType();
      this.getStudenti();
    } else {
      this.$router.push("/Login");
    }
  },
  methods: {
    getStudenti() {
      axios
        .get(Vue.prototype.$adresa + "getStudent", {
          headers: {
            Authorization: Vue.prototype.$pristupniToken,
          },
        })
        .then((res) => {
          this.studenti = res.data;
        });
    },
    checkForm() {
      if (
        /^[a-žA-Ž]+$/.test(this.student.Ime) &&
        /^[a-žA-Ž]+$/.test(this.student.Prezime)
      ) {
        if (
          this.student.Ime.length > 2 &&
          this.student.Prezime.length > 2 &&
          this.student.Brojindeksa > 999 &&
          this.student.Brojindeksa < 10000
        ) {
          for (let i = 0; i < this.studenti.length; i++) {
            if (this.studenti[i].Studentid != this.$route.params.Id) {
              if (this.student.Brojindeksa == this.studenti[i].Brojindeksa) {
                this.poruka = "Uneseni broj indeksa već postoji";
                this.showErrorDialog = true;
                return;
              }
            }
          }
          this.showInfoDialog = true;
        } else {
          this.poruka = "Unijeti podaci nisu ispravni";
          this.showErrorDialog = true;
        }
      } else {
        if (this.student.Ime.length > 0 && this.student.Prezime.length > 0) {
          this.poruka =
            "Ime i prezime ne smiju da sadrže brojeve ili druge znakove";
        } else this.poruka = "Ime i prezime moraju da budu uneseni";
        this.showErrorDialog = true;
      }
    },
    addStudent() {
      if (this.$route.params.Id == 0) {
        /*axios.get(Vue.prototype.$adresa + "addStudent", {
          params: {
            ime: this.student.Ime,
            prezime: this.student.Prezime,
            status: this.student.Statusstudentaid,
            godina: this.student.Godina,
            br_indeksa: this.student.Brojindeksa,
          },

        });*/
        axios.post(Vue.prototype.$adresa + "addStudentPost", this.student, {
          headers: {
            Authorization: Vue.prototype.$pristupniToken,
          },
        });
        this.$router.push("/Korisnici");
      } else {
        /* axios.get(Vue.prototype.$adresa + "editStudent", {
          params: {
            id: this.$route.params.Id,
            ime: this.student.Ime,
            prezime: this.student.Prezime,
            status: this.student.Statusstudentaid,
            godina: this.student.Godina,
            br_indeksa: this.student.Brojindeksa,
          },
          });
          */
        axios.post(Vue.prototype.$adresa + "editStudentPost", this.student, {
          headers: {
            Authorization: Vue.prototype.$pristupniToken,
          },
        });
        this.$router.push("/Korisnici");
      }
    },
    checkType() {
      if (this.$route.params.Id != 0) {
        axios
          .get(Vue.prototype.$adresa + "ViewStudent", {
            params: { id: this.$route.params.Id },
            headers: {
              Authorization: Vue.prototype.$pristupniToken,
            },
          })
          .then((res) => {
            this.student = res.data;
          });
      }
    },
  },
};
</script>
