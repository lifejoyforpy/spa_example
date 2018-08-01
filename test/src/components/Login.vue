<template>
  <div id="Login">
    <x-header :left-options="{showBack: false}">
      登录页面
    </x-header>
    <group>
      <flexbox>
        <flexbox-item :span="1/12">
          <i class="icon iconfont1 icon-denglu"></i>
        </flexbox-item>
        <flexbox-item :span="11/12">
          <x-input title="用户名" name="username" v-model="User.user" placeholder="请输入用户名">
          </x-input>
        </flexbox-item>
      </flexbox>
    </group>
    <group>
      <flexbox>
        <flexbox-item :span="1/12">
          <i class="icon iconfont1 icon-mima"></i>
        </flexbox-item>
        <flexbox-item :span="11/12">
          <x-input title="密码 " type="password" placeholder="请输入密码" v-model="User.password" :min="1" :max="12"></x-input>
        </flexbox-item>
      </flexbox>
       <toast v-model="showPositionValue" type="warn" :time="1000" is-show-mask text="账户名或者密码错误"></toast>
    </group>
    <group>
      <x-button @click.native="login">
        登录
      </x-button>
    </group>
  </div>
</template>
<script>
import {mapGetters, mapMutations, mapActions} from 'vuex';
import { XInput, Group, XHeader, Flexbox, FlexboxItem, XButton,Toast  } from 'vux'
export default {
  components: {
    XInput,
    Group,
    XHeader,
    Flexbox,
    FlexboxItem,
    XButton,
    Toast 
  },
  methods: {
      ...mapGetters([
        // 在store.js 中注册的getters
        'login'
      ]),
    login() {
      this.$axios.post("/api/StaffManager/login", {UserJobNo:this.User.user,Password:this.User.password}).then((response) => {
        if (response.data.flag == '1') {
          this.$store.dispatch('login',response.data.data)
          this.$router.push({
            path: '/HelloWorld'
          })
        }
        else{
           console.info('login fail')
          this.showPositionValue=true
        }
      }).catch(
          
          function (error) {
            console.log(error)
           
          }
          )


    }
  },
  data() {
    return {
      User: {
        user: '',
        password: ''
      },
      showPositionValue:false
    }
  }
}
</script>
<style scoped>
.iconfont1 {
  font-family: "iconfont" !important;
  font-size: 30px;
  font-style: normal;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
</style>

