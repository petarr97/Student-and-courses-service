import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import KorisniciIndex from "../components/Korisnici/KorisniciIndex.vue";
import UnosKorisnika from "../components/Korisnici/UnosKorisnika.vue";
import PrikazKorisnika from "../components/Korisnici/PrikazKorisnika.vue";
import KursLista from "../components/Kurs/KursLista.vue";
import Kursevi from "../components/Kurs/Kursevi.vue";
import DodavanjeKursa from "../components/Kurs/DodavanjeKursa.vue";
import VuejsDialog from "vuejs-dialog";
import Login from "../components/Korisnici/Login.vue";
import Registracija from "../components/Korisnici/Registracija.vue";

Vue.use(VueRouter);
Vue.use(VuejsDialog);
const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/about",
    name: "About",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/About.vue"),
  },
  {
    path: "/Korisnici",
    name: "KorisniciIndex",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: KorisniciIndex,
  },
  {
    path: "/UnosKorisnika/:Id",
    name: "UnosKorisnika",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: UnosKorisnika,
  },
  {
    path: "/PrikazKorisnika/:Id",
    name: "PrikazKorisnika",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: PrikazKorisnika,
  },
  {
    path: "/Kursevi",
    name: "Kursevi",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: Kursevi,
  },
  {
    path: "/kursdetaljno/:Id",
    name: "KursLista",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: KursLista,
  },
  {
    path: "/dodavanjeKursa",
    name: "DodavanjeKursa",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: DodavanjeKursa,
  },
  {
    path: "/Login",
    name: "Login",
    component: Login,
  },
  {
    path: "/Registracija",
    name: "Registracija",
    component: Registracija,
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
