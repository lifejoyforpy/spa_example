<template>
  <div id="staffonlie">
    <x-header :right-options="{showMore: true}" @on-click-more="showMenus = true">人员上班管理</x-header>
    <br>
    <div id="staffinfo">
      <flexbox>
        <flexbox-item>
          <cell title="当前线别:" v-model="staffmodel.line"></cell>
        </flexbox-item>
        <flexbox-item>
          <popup-picker title="班别：" :data="shiftlist" v-model="staffmodel.shift" @on-show="onShow" @on-hide="onHide" @on-change="onChange"></popup-picker>
        </flexbox-item>
        <flexbox-item>
          <x-button type="primary" link='/staffonline' style="background-color:#35495e">刷新</x-button>
        </flexbox-item>
      </flexbox>
      <br>
      <flexbox>
        <flexbox-item :span="1/3">
          <cell title="登录班线长:" value-align="center" v-model="userinfo.username"></cell>
        </flexbox-item>
      </flexbox>
      <br>
      <tab>
        <tab-item selected @click.native="onItemClick('0')">
          未点名人员
        </tab-item>
        <tab-item @click.native="onItemClick('1')">
          已点名人员
        </tab-item>
      </tab>
      <br>

      <div id="onlinelist" v-if="seen=='OK'">
        <flexbox>
          <flexbox-item>
            <cell :title="'当线人员:    '+online_num+'人'"></cell>
          </flexbox-item>
          <flexbox-item>
            <x-button type="primary" @click.native="checkAll" style="background-color:#35495e">全选</x-button>
          </flexbox-item>
          <flexbox-item>
            <x-button type="primary" @click.native="invertCheck" style="background-color:#35495e">反选</x-button>
          </flexbox-item>
          <flexbox-item>
            <x-button type="primary" @click.native="checkNone" style="background-color:#35495e">全不选</x-button>
          </flexbox-item>
        </flexbox>
        <br>
        <div>
          <scroller height="850px" scrollbar-y>
            <x-table>
              <thead>
                <tr>
                  <th>序号</th>
                  <th>工号</th>
                  <th>人员</th>
                  <th>岗位</th>
                  <th>点名</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(item,index) in onlinelist">
                  <td v-model="item.id">
                    {{index+1}}
                  </td>
                  <td>
                    {{item.empno}}
                  </td>
                  <td>
                    {{item.empname}}
                  </td>
                  <td>
                    {{item.job}}
                  </td>
                  <td>
                    <input type="checkbox" v-model="item.check " name="Checkbox">
                  </td>
                </tr>
              </tbody>
            </x-table>
          </scroller>
        </div>
        <div>
          <flexbox :gutter="8">
            <flexbox-item :span="1/3" :order="1">
              <x-button type="primary" @click.native="gowork" style="background-color:#35495e">上班</x-button>
            </flexbox-item>
            <flexbox-item :span="1/3" :order="3">
              <x-button type="primary" @click.native="transwork" style="background-color:#35495e">调岗</x-button>
              <div v-transfer-dom>
                <x-dialog v-model="showScrollBox" class="dialog-demo">
                  <p class="dialog-title">调岗</p>
                  <div class="img-box" style="height:150px;padding:15px 0;overflow:scroll;-webkit-overflow-scrolling:touch;">
                    <selector ref="defaultValueRef" title="目标线别:" direction="rtl" :options="linelist" v-model="new_line"></selector>
                  </div>
                  <toast v-model="showtoast" type="warn" :time="1500" :text="promt" position="middle"></toast>
                  <flexbox>
                    <flexbox-item>
                      <x-button type="default" @click.native="linecancel">取消</x-button>
                    </flexbox-item>
                    <flexbox-item>
                      <x-button type="primary" @click.native="linecheck(new_line)" style="background-color:#35495e">确定</x-button>
                    </flexbox-item>
                  </flexbox>
                </x-dialog>
              </div>
            </flexbox-item>
          </flexbox>
        </div>
      </div>
      <div id="onlinetable" v-else>
        <div>
          <flexbox>
            <flexbox-item :span="1/3">
              <cell :title="'已点名人员:    '+roll_num+'人'"></cell>
            </flexbox-item>
          </flexbox>
          <scroller height="850px" scrollbar-y>
            <x-table>
              <thead>
                <tr>
                  <th>序号</th>
                  <th>工号</th>
                  <th>人员</th>
                  <th>岗位</th>
                  <th>状态</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(item,index) in rolllist">
                  <td v-model="item.id">
                    {{index+1}}
                  </td>
                  <td>
                    {{item.empno}}
                  </td>
                  <td>
                    {{item.empname}}
                  </td>
                  <td>
                    {{item.job}}
                  </td>
                  <td v-if="item.Status_flag=='0'">
                    上班
                  </td>
                  <td v-if="item.Status_flag=='1'">
                    离岗
                  </td>
                  <td v-if="item.Status_flag=='2'">
                    下班
                  </td>
                </tr>
              </tbody>
            </x-table>
          </scroller>
        </div>

      </div>
    </div>
    <actionsheet v-model="showMenus" :menus="menus" @on-click-menu-delete="logout()" show-cancel></actionsheet>
  </div>
  </div>
</template>
<script>

import { mapGetters } from 'vuex'
import { PopupPicker, Group, Cell, Picker, XButton, Flexbox, FlexboxItem, XTable, XHeader, Tab, TabItem, Scroller, XDialog, TransferDomDirective as TransferDom, Selector, Toast, Actionsheet } from 'vux'
// import { constants } from 'http2';
export default {
  components: {
    PopupPicker,
    Group,
    Picker,
    XButton,
    Cell,
    Flexbox,
    FlexboxItem,
    XTable,
    XHeader,
    Tab,
    TabItem,
    Scroller,
    XDialog,
    Selector,
    Toast,
    Actionsheet
  },
  mounted() {
    console.log('mounted')
    this.$axios.post('/api/StaffManager/getOnlieInfo', { shift: this.staffmodel.shift[0], line: this.staffmodel.line, user: this.userinfo.username }).then((response) => {
      console.log(response.data)
      if (response.data.flag == 1) {
        console.log(response.data.data[0].line)
        this.staffmodel.line = response.data.data[0].line
        this.onlinelist = response.data.data
        this.online_num = response.data.data.length
        console.log(this.onlinelist)

      }
    }).catch(
      function (error) {
        console.log(error)
      }
      );
    this.getlinelist();
  },
  computed: {
    ...mapGetters([
      'userinfo'
    ])

  }
  ,
  methods: {
    logout() {
      console.info('logout')
      this.$store.dispatch('logout')
      this.$router.push({
        path: '/'
      })
    },
    linecancel() {
      this.showScrollBox = false;
    }
    ,
    onChange(val) {
      console.log('val change', val)
    },
    onItemClick(status) {
      if (status == '0') {
        this.seen = 'OK'
      }
      if (status == '1') {
        console.log("jinru")
        this.seen = 'ERROR'
        this.$axios.post('/api/StaffManager/getOnlieInfo', { shift: this.staffmodel.shift[0], line: this.staffmodel.line, user: this.staffmodel.user, status: '1' }).then((response) => {
          console.info(response.data)

          this.rolllist = response.data.data;
          this.roll_num = response.data.data.length;

        }
        ).catch(
          function (error) {
            console.log(error)
          }
          )
      }
    },
    onShow() { },
    onHide() {

    },
    stringcombine(str1, str2) {
      return str1, str2
    },
    checkAll() {
      console.info("123")
      if (this.onlinelist.length > 0) {
        this.onlinelist.forEach(element => {
          element.check = true;
        });

      }
    },
    invertCheck() {

      if (this.onlinelist.length > 0) {
        this.onlinelist.forEach(element => {
          element.check = !element.check;
        });

      }
    },
    checkNone() {
      if (this.onlinelist.length > 0) {
        this.onlinelist.forEach(element => {
          element.check = false;
        });

      }
    },
    gowork() {

      var list = []
      var linelist = []
      this.onlinelist.forEach((item, index) => {
        if (item.check) {
          list.push(item)
        }
        else {
          linelist.push(item)

        }
      })
      console.log(list)
      if (list.length == 0)
        return;
      this.$axios.post('/api/StaffManager/goWork', list).then((response) => {

        console.log(response.data)
        if (response.data.flag == 1) {
          console.info('test')
          this.onlinelist = linelist
          console.log(this.onlinelist)
        }
      }).catch(
        function (error) {
          console.log(error)
        }
        )

    },
    transwork() {
      var templine = []
      this.onlinelist.forEach(item => {
        if (item.check) {
          templine.push(item)
        }
      });
      if (templine.length > 0)
        this.showScrollBox = true
    },
    linecheck(obj) {

      var checkline = [];
      var uncheline = [];
      console.log(obj);
      if (obj == "") {
        this.promt = '线别不能为空'
        this.showtoast = true
        return
      }
      if (obj == this.staffmodel.line) {
        this.promt = '调岗线别不能为当前线别'
        this.showtoast = true
        return
      }
      this.onlinelist.forEach(item => {
        if (item.check) {
          item.new_line = obj
          checkline.push(item)
        }
        else {
          uncheline.push(item)
        }
      });
      console.log(checkline);
      this.tanspost(checkline, uncheline);
      //调岗
      this.showScrollBox = false
    },
    //获取线别
    getlinelist() {
      this.$axios.post('/api/StaffManager/getLine', {}).then((response) => {
        console.log(response.data)
        if (response.data.flag == 1) {
          console.log("getline")
          this.linelist = response.data.data
          this.new_line = this.linelist[0]
          console.log(this.linelist)

        }
      }).catch(
        function (error) {
          console.log(error)
        }
        )

    },
    tanspost(obj1, obj2) {
      console.log(obj1);
      this.$axios.post('/api/StaffManager/transWork', obj1).then((response) => {
        console.log(response.data)
        if (response.data.flag == 1) {
          console.info('tanspost')
          this.onlinelist = obj2
          console.log(this.onlinelist)
        }
      }).catch(
        function (error) {
          console.log(error)
        }
        )

    }


  },
  directives: {
    TransferDom
  },
  data() {
    return {
      staffmodel:
        {
          shift: ['白班'],
          line: '',
          user: '张三一'
        },
      shiftlist: [['白班', '晚班']],
      offworklist: [],
      onlinelist: [],
      rolllist: [],
      online_num: 0,
      roll_num: 0,
      seen: 'OK',
      showScrollBox: false,
      new_line: '',
      linelist: [],
      promt: '',
      showtoast: false,
      showMenus: false,
      menus:{
        delete:'<span style=" color:red ">退出</span>'
      }

    }

  }

}
</script>
<style>

</style>

