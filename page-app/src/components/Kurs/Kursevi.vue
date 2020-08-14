<template>
  <div class="row">
    <div class="col-8 offset-2">
      <table class="table table-striped">
        <thead class="thead-dark">
          <tr>
            <th>#</th>
            <th>Naziv Kursa</th>
            <th></th>
          </tr>
          <tr v-for="(kurs, ind) in kursevi" :key="ind">
            <td>{{ ind + 1 }}</td>
            <td>{{ kurs.Nazivkursa }}</td>
            <td>
              <button class="btn btn-info" @click="prikaziKurs(kurs.Kursid)">
                Detaljno
              </button>
            </td>
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
      kursevi: [],
    };
  },
  updated() {
    if (Vue.prototype.$ulogovan) {
      axios
        .get(Vue.prototype.$adresa + "getKursevi", {
          headers: {
            Authorization: Vue.prototype.$pristupniToken,
          },
        })
        .then((res) => {
          this.kursevi = res.data;
        });
    } else this.$router.push("/Login");
  },
  created() {
    if (Vue.prototype.$ulogovan) {
      axios
        .get(Vue.prototype.$adresa + "getKursevi", {
          headers: {
            Authorization: Vue.prototype.$pristupniToken,
          },
        })
        .then((res) => {
          this.kursevi = res.data;
        });
    } else this.$router.push("/Login");
  },
  methods: {
    prikaziKurs(broj) {
      var routerString = "/kursdetaljno/" + broj;
      this.$router.push(routerString);
    },
  },
};
</script>
<style scoped>
td {
  font-weight: bold;
}
</style>
