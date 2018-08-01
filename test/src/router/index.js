import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'
import StaffOnline from '@/components/StaffOnline'
import StaffOffline from '@/components/StaffOffline'
import Login from '@/components/Login'
Vue.use(Router)   
//页面级别拦截
export default new Router({
  routes: [
    {
      path: '/',
      name: 'Login',
      component: Login
    },
    {

      path: '/HelloWorld',
      name: 'HelloWorld',
      component: HelloWorld
    },
    {

      path: '/StaffOnline',
      name: 'StaffOnline',
      component: StaffOnline
    },
    {

      path: '/StaffOffline',
      name: 'StaffOffline',
      component: StaffOffline
    }
  ]
})
