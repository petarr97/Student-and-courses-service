<template>
  <div class="row">
    <div class="col-8 offset-2">
      <table class="table table-striped">
        <thead class="thead-dark">
          <tr>
            <th>#</th>
            <th>Ime</th>
            <th>Prezime</th>
            <th></th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tr v-for="(obj, ind) in students" :key="ind">
          <td>{{ ind + 1 }}</td>
          <td>{{ obj.Ime }}</td>
          <td>{{ obj.Prezime }}</td>
          <td>
            <button
              type="button"
              class="btn btn-primary"
              @click="editStudent(obj.Studentid)"
            >
              Izmjeni
            </button>
          </td>
          <td>
            <button
              type="button"
              class="btn btn-danger"
              @click="postaviId(obj.Studentid), (showDeleteModal = true)"
            >
              Izbrisi
            </button>
          </td>
          <td>
            <button
              type="button"
              class="btn btn-info"
              @click="showStudent(obj.Studentid)"
            >
              Detaljno
            </button>
          </td>
        </tr>
      </table>

      <div id="overlay" v-if="showDeleteModal">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title">
                Jeste li sigurni da zelite da potvrdite brisanje?
              </h5>
              <button
                type="button"
                class="close"
                @click="showDeleteModal = false"
              >
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body p-4">
              <button
                class="btn btn-info btn-lg"
                @click="(showDeleteModal = false), deleteStudent()"
              >
                Da
              </button>
              <button
                class="btn btn-danger btn-lg"
                @click="showDeleteModal = false"
              >
                Ne
              </button>
            </div>
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
      name: "",
      students: [],
      id: "",
      dialog: true,
      showDeleteModal: false,
      StudentID: "",
    };
  },
  created() {
    if (Vue.prototype.$ulogovan == true) {
      this.showTable();
    } // else this.$router.push("/Login");
  },
  updated() {
    if (Vue.prototype.$ulogovan == true) {
      this.showTable();
    } //else this.$router.push("/Login");
  },
  methods: {
    showTable() {
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
    deleteStudent() {
      axios
        .get(Vue.prototype.$adresa + "removeStudent", {
          params: { id: this.StudentID },
          headers: {
            Authorization: Vue.prototype.$pristupniToken,
          },
        })
        .then((res) => {
          this.students = res.data;
        });
    },
    editStudent(broj) {
      var routerString = "/UnosKorisnika/" + broj;
      this.$router.push(routerString);
    },
    showStudent(broj) {
      var routerString = "/PrikazKorisnika/" + broj;
      this.$router.push(routerString);
    },
    postaviId(broj) {
      this.StudentID = broj;
    },
  },
};
</script>
<style scoped>
table:nth-child(even) {
  background: grey;
}
td {
  font-weight: bold;
}
button {
  margin-left: 1ch;
  margin-right: 1ch;
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
