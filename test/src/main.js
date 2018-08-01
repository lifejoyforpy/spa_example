// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import FastClick from 'fastclick'
import App from './App'
import router from './router/index.js'
import axios from 'axios'
import store from './script/store.js'

// const routes = [{
//   path: '/',
//   component: Home
// }]

// const router = new VueRouter({
//   routes
// })

FastClick.attach(document.body)

Vue.config.productionTip = false

Vue.prototype.$axios = axios
router.beforeEach((to, from, next) => {
  console.log(to.path)
  if (to.path === '/') {
    next()
  }
  else {
    console.log(store.state.islogin)
    if (!localStorage.getItem('islogin')) {
      console.info('router')
      next('/')
    }
    else {  
      console.info('页面')
      next()
    }
  }

})
/* eslint-disable no-new */
new Vue({
  router,
  store,
  beforeCreate(){
    console.info('beforeCreate')
    this.$store.commit('initialiseStore')
  },
  render: h => h(App)
}).$mount('#app-box')
