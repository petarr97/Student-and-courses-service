<template>
  <div class="row">
    <div class="col-8 offset-2">
      <div>
        <h1>{{ imeKursa }}</h1>
      </div>
      <table class="table table-striped">
        <thead class="thead-dark">
          <tr>
            <th>#</th>
            <th>Ime</th>
            <th>Prezime</th>
            <th>Broj Indeksa</th>
          </tr>
          <tr v-for="(stud, ind) in studenti2" :key="ind">
            <td>{{ ind + 1 }}</td>
            <td>{{ stud.Ime }}</td>
            <td>{{ stud.Prezime }}</td>
            <td>{{ stud.Brojindeksa }}</td>
          </tr>
        </thead>
      </table>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Vue from "vue";

export default {
  data() {
    return {
      imeKursa: "",
      studenti: [],
      lista: [],
      studenti1: [],
      studenti2: [],
    };
  },
  created() {
    if (Vue.prototype.$ulogovan) this.procitajImeKursa();
    // else this.$router.push("/Login");
  },
  methods: {
    async procitajImeKursa() {
      axios
        .get(Vue.prototype.$adresa + "getKurs", {
          params: { id: this.$route.params.Id },
          headers: {
            Authorization: Vue.prototype.$pristupniToken,
          },
        })
        .then((res) => {
          this.imeKursa = res.data.Nazivkursa;
          this.procitajStudente();
        });
    },
    procitajStudente() {
      axios
        .get(Vue.prototype.$adresa + "getStudentKursList", {
          params: { id: this.$route.params.Id },
          headers: {
            Authorization: Vue.prototype.$pristupniToken,
          },
        })
        .then((res) => {
          this.studenti = res.data;
          this.prikazisveStudent();
        });
    },
    prikazisveStudent() {
      axios
        .get(Vue.prototype.$adresa + "getStudent", {
          headers: {
            Authorization: Vue.prototype.$pristupniToken,
          },
        })
        .then((res) => {
          this.studenti1 = res.data;
          this.postaviStudenteKojiSlusajuKurs();
        });
    },

    postaviStudenteKojiSlusajuKurs() {
      for (var i = 0; i < this.studenti.length; i++) {
        if (this.studenti[i].Kursid != this.$route.params.Id) {
          this.studenti.splice(i, 1);
          i--;
        }
      }

      for (i = 0; i < this.studenti.length; i++) {
        for (var j = 0; j < this.studenti1.length; j++) {
          if (this.studenti[i].Studentid == this.studenti1[j].Studentid)
            this.studenti2.push(this.studenti1[j]);
        }
      }
    },
  },
};
</script>

<style scoped></style>
