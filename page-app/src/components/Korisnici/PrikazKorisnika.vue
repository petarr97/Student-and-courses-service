<template>
  <div class="row">
    <div class="col-8 offset-2">
      <table class="table table-striped">
        <thead class="thead-dark">
          <tr>
            <th colspan="2">Detalji o korisniku</th>
          </tr>
        </thead>
        <tr>
          <td>Ime</td>
          <td>{{ student.Ime }}</td>
        </tr>
        <tr>
          <td>Prezime</td>
          <td>{{ student.Prezime }}</td>
        </tr>
        <tr>
          <td>BrojIndeksa</td>
          <td>{{ student.Brojindeksa }}</td>
        </tr>
        <tr>
          <td>Godina</td>
          <td>{{ student.Godina }}</td>
        </tr>
        <tr>
          <td>Status</td>
          <td>{{ status }}</td>
        </tr>
      </table>

      <table class="table table-striped">
        <thead class="thead-dark">
          <tr>
            <th colspan="2">
              Kursevi koje pohadja korisnik {{ student.Ime }}
              {{ student.Prezime }}
            </th>
          </tr>
        </thead>
        <tr v-for="(kurs, ind) in prikazKurseva" :key="ind">
          <td>{{ kurs.Nazivkursa }}</td>
        </tr>
      </table>
    </div>
  </div>
</template>
<script>
import axios from "axios";
import Vue from "vue";

// eslint-disable-next-line no-unused-labels
export default {
  data() {
    return {
      student: {
        Ime: "",
        Prezime: "",
        StudentId: "",
        Brojindeksa: "",
        Godina: "",
        Statusstudentaid: "",
      },
      showInfoDialog: true,
      students: {},
      kursevi: [],
      sviKursevi: [],
      prikazKurseva: [],
      status: "",
      proba: 1,
    };
  },

  created() {
    if (Vue.prototype.$ulogovan) {
      this.showStudent(this.$route.params.Id);
      this.prikaziKurseve();
    } else this.$router.push("/Login");
  },
  methods: {
    showStudent(broj) {
      axios
        .get(Vue.prototype.$adresa + "ViewStudent", {
          params: { id: broj },
          headers: {
            Authorization: Vue.prototype.$pristupniToken,
          },
        })
        .then((res) => {
          this.student = res.data;
          this.provjeriStatus(this.student.Statusstudentaid);
        });
    },
    prikaziKurseve() {
      axios
        .get(Vue.prototype.$adresa + "getStudentKursList", {
          params: { id: this.$route.params.Id },
          headers: {
            Authorization: Vue.prototype.$pristupniToken,
          },
        })
        .then((res) => {
          this.kursevi = res.data;
          this.prikazisveKurseve();
        });
    },
    prikazisveKurseve() {
      axios
        .get(Vue.prototype.$adresa + "getKursevi", {
          headers: {
            Authorization: Vue.prototype.$pristupniToken,
          },
        })
        .then((res) => {
          this.sviKursevi = res.data;
          this.postaviKurseve();
        });
    },
    postaviKurseve() {
      for (var i = 0; i < this.kursevi.length; i++) {
        if (this.kursevi[i].Studentid != this.$route.params.Id) {
          this.kursevi.splice(i, 1);
          i--;
        }
      }
      for (i = 0; i < this.kursevi.length; i++) {
        for (var j = 0; j < this.sviKursevi.length; j++) {
          if (this.kursevi[i].Kursid == this.sviKursevi[j].Kursid)
            this.prikazKurseva.push(this.sviKursevi[j]);
        }
      }
    },
    provjeriStatus(broj) {
      console.log("broj " + broj);
      if (broj == 1) this.status = "Redovan";
      else this.status = "Vanredan";
    },
  },
};
</script>

<style scoped>
td {
  font-weight: bold;
}
</style>
