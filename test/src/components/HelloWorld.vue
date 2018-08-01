<template>
  <div class="main">
  <x-header>
     Home
  </x-header>
  <div class="hello">
    <ul>
      <li>
      <router-link id="StaffOnline"  to="/StaffOnline" >    
       <i class="icon iconfont icon-gongzuoshangban"></i>
       </i>
       <div >
         人员上班管理
       </div>
      </router-link>
      </li>
      <li>
      <router-link id="StaffOffline"  to="/StaffOffline" >
       <i class="icon iconfont icon-gangweiguanli"></i>
       </i>
       <div >
         人员在岗管理
       </div>
      </router-link>
      </li>
    </ul> 
  </div>
  </div>
</template>

<script>
import { XHeader,XButton } from 'vux'

var myVue={}
export default {
  name: 'HelloWorld',
  components: {
    XHeader,
    XButton
  },
  data () {
    return {
      msg: 'Welcome to Your Vue.js App'
    }
  },
 
  
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1, h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
.user {
  width: 100px;
  height: 100px
}
</style>
