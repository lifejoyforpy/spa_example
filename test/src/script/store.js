import Vue from 'vue'
import Vuex from 'vuex'
Vue.use(Vuex)
// 创建基本状态
const state = {
 // 登录状态为没登录
 islogin: false,
 // 用户信息数据,目前只需要avatar和name,还是把username也加上吧
 userinfo: {
  username: '',
  userid:'',
  cookiedate:''
 }
}
// 创建改变状态的方法
const mutations = {
 // 改变状态的方法也需要2个，一个是登录或注册了，一个是登出了
 // 这里不能写箭头函数???
 // 登录
 login (state,data) {
  // 先让登录状态变为登录了
  console.info('已经到这')
  console.info(state)
  if(!state.islogin) 
       state.islogin=true
  // 然后去sessionStorage取用户数据
  // 再把用户数据发下去
  state.userinfo.userid =data.UserJobNo
  state.userinfo.username=data.UserName
  state.userinfo.cookiedate = new Date();
  //存一份到localstorage
  localStorage.setItem("userinfo",JSON.stringify(state.userinfo))
  localStorage.setItem("islogin", true)
 },
 // 登出
 logout (state) {
  // 这个同理
  state.islogin = false
  state.userinfo.username = ''
  state.userinfo.cookiedate = ''
  state.userinfo.userid=''
  localStorage.removeItem("userinfo")
  localStorage.removeItem("islogin")
 },
 initialiseStore(state){
   
    console.log('initialiseStore')
    console.log(JSON.parse(localStorage.getItem('userinfo')))
    if(localStorage.getItem('userinfo'))
    {    
        console.log(localStorage.getItem('islogin'))
        this.replaceState(Object.assign(state.userinfo,JSON.parse(localStorage.getItem('userinfo'))))
        this.replaceState(Object.assign(state,{ islogin :JSON.parse(localStorage.getItem('islogin'))}))
        console.log(state)
    }
 }
}
// 创建驱动actions可以使得mutations得以启动
const actions = {
 // 这里先来一个驱动LOGIN的东西就叫login吧
 // 这个context是官方写的，应该叫什么无所谓
 login (context,data) {
  context.commit('login',data)
 },
 // 同样来个logout
 logout (context) {
  context.commit('logout')
 }
}

// 获取状态信息
const getters = {
   userinfo: state =>{ 
       return state.userinfo
    }
}
const myPlugin = store => {
    // 当 store 初始化后调用
    store.subscribe((mutation, state) => {
      // 每次 mutation 之后调用
      // mutation 的格式为 { type, payload }
    })
  }
export default new Vuex.Store({
 state,
 mutations,
 actions,
 getters
 
})