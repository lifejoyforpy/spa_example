<template>
  <div id="staffoffline">
    <x-header :right-options="{showMore: true}" @on-click-more="showMenus = true">人员在岗管理</x-header>
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
          <x-button type="primary" link='/Staffoffline' style="background-color:#35495e">刷新</x-button>
        </flexbox-item>
      </flexbox>
      <br>
      <flexbox>
        <flexbox-item :span="1/3">
          <cell title="登录班线长:" v-model="userinfo.username"></cell>
        </flexbox-item>
      </flexbox>
      <br>
      <tab>
        <tab-item selected @click.native="onItemClick('0')">
          在岗人员
        </tab-item>
        <tab-item @click.native="onItemClick('1')">
          离岗人员
        </tab-item>
      </tab>
      <br>
      <flexbox>
        <flexbox-item :span="1/3" v-if="seen=='OK'">
          <cell :title="'在岗人员 '+ onpost_num+'人' "></cell>
        </flexbox-item>
        <flexbox-item :span="1/3" v-else>
          <cell :title="'离岗人员 '+ offpost_num +'人' "></cell>
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
      <div id="onlinelist" v-if="seen=='OK'">
        <div>
          <scroller height="850px" scrollbar-y>
            <x-table>
              <thead>
                <tr>
                  <th>序号</th>
                  <th>工号</th>
                  <th>人员</th>
                  <th>岗位</th>
                  <th>人员来源</th>
                  <th>选择</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(item,index) in onpostlist">
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
                    {{item.source_line}}
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
              <x-button type="primary" @click.native="ligang" style="background-color:#35495e">离岗</x-button>
              <div v-transfer-dom>
                <x-dialog v-model="showScrollBox" class="dialog-demo">
                  <p class="dialog-title">离岗</p>
                  <div class="img-box" style="height:150px;padding:15px 0;overflow:scroll;-webkit-overflow-scrolling:touch;">
                    <selector ref="defaultValueRef" title="离岗原因" direction="rtl" :options="reasonlist" v-model="ligang_mark"></selector>
                  </div>
                  <toast v-model="showtoast" type="warn" :time="1500" :text="promt" position="middle"></toast>
                  <flexbox>
                    <flexbox-item>
                      <x-button type="default" @click.native="ligangcancel">取消</x-button>
                    </flexbox-item>
                    <flexbox-item>
                      <x-button type="primary" @click.native="ligangselect(ligang_mark)" style="background-color:#35495e">确定</x-button>
                    </flexbox-item>
                  </flexbox>
                </x-dialog>
              </div>
            </flexbox-item>
            <flexbox-item :span="1/3" :order="2">
              <x-button type="primary" @click.native="offwork" style="background-color:#35495e">下班</x-button>
            </flexbox-item>
            <flexbox-item :span="1/3" :order="3">
              <x-button type="primary" @click.native="transjob1" style="background-color:#35495e">调岗</x-button>
              <div v-transfer-dom>
                <x-dialog v-model="transjob" class="dialog-demo">
                  <p class="dialog-title">调岗</p>
                  <div class="img-box" style="height:150px;padding:15px 0;overflow:scroll;-webkit-overflow-scrolling:touch;">
                    <selector ref="defaultValueRef" title="目标线别" direction="rtl" :options="linelist" v-model="new_line"></selector>
                  </div>
                  <toast v-model="showtoast1" type="warn" :time="1500" :text="promt1" position="middle"></toast>
                  <flexbox>
                    <flexbox-item>
                      <x-button type="default" @click.native="linecancel">取消</x-button>
                    </flexbox-item>
                    <flexbox-item>
                      <x-button type="primary" @click.native="lineselect(new_line)" style="background-color:#35495e">确定</x-button>
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
          <scroller height="850px" scrollbar-y>
            <x-table>
              <thead>
                <tr>
                  <th>序号</th>
                  <th>工号</th>
                  <th>人员</th>
                  <th>离岗原因</th>
                  <th>选择</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(item,index) in offpostlist">
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
                    {{item.ligang_mark}}
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
              <x-button type="primary" @click.native="returnpost" style="background-color:#35495e">回岗</x-button>
            </flexbox-item>
          </flexbox>
        </div>
      </div>
      <actionsheet v-model="showMenus" :menus="menus" @on-click-menu-delete="logout()" show-cancel></actionsheet>

    </div>

  </div>
  </div>
</template>
<script>
import { PopupPicker, Group, Cell, Picker, XButton, Flexbox, FlexboxItem, XTable, XHeader, Tab, TabItem, Scroller, XDialog, TransferDomDirective as TransferDom, Selector, Toast, Actionsheet } from 'vux'
import { fail } from 'assert';
import { mapGetters } from 'vuex'
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
    this.$axios.post('/api/StaffManager/getpostinfo', { shift: this.staffmodel.shift[0], line: this.staffmodel.line, user: this.userinfo.username }).then((response) => {
      console.log(response.data)
      if (response.data.flag == 1) {
        console.log(this)
        console.log(response.data.data[0].line)
        this.staffmodel.line = response.data.data[0].line
        this.onpostlist = response.data.data
        this.onpost_num = response.data.data.length;
        console.log(this.onpostlist)

      }
    }).catch(
      function (error) {
        console.log(error)
      }
      )
    this.getoffreasonlist()
    this.getlinelist()
  },
  computed: {
    ...mapGetters([
      'userinfo'
    ])

  },
  methods: {
    logout() {
      console.info('logout')
      this.$store.dispatch('logout')
      this.$router.push({
        path: '/'
      })
    },
    ligang() {
      var templine = []
      this.onpostlist.forEach(element => {
        if (element.check) {

          templine.push(element);
        }

      })
      if (templine.length > 0)
        this.showScrollBox = true
    }
    ,
    transjob1() {
      var templine = []
      this.onpostlist.forEach(element => {
        if (element.check) {

          templine.push(element);
        }

      })
      if (templine.length > 0)
        this.transjob = true
    },
    //获取在岗信息函数
    getonpost() {
      this.$axios.post('/api/StaffManager/getpostinfo', { shift: this.staffmodel.shift[0], line: this.staffmodel.line, user: this.staffmodel.user }).then((response) => {
        console.log(response.data)
        if (response.data.flag == 1) {
          console.log(this)
          console.log(response.data.data[0].line)
          this.staffmodel.line = response.data.data[0].line
          this.onpostlist = response.data.data
          this.onpost_num = response.data.data.length;
          console.log(this.onpostlist)

        }
      }).catch(
        function (error) {
          console.log(error)
        }
        )


    },
    onChange(val) {
      console.log('val change', val)
    },
    onItemClick(status) {
      if (status == '0') {
        console.info("test1")
        this.getonpost();
        this.seen = 'OK'
      }
      if (status == '1') {

        this.seen = 'ERROR'
        this.$axios.post('/api/StaffManager/getpostinfo', { shift: this.staffmodel.shift[0], line: this.staffmodel.line, user: this.staffmodel.user, status: '1' }).then(
          (response) => {

            console.log(response.data)
            this.offpostlist = response.data.data
            this.offpost_num = response.data.data.length
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
    checkAll() {
      console.info("123")
      if (this.seen == 'OK') {
        if (this.onpostlist.length > 0) {
          this.onpostlist.forEach(element => {
            element.check = true;
          })

        }
      }
      else {

        if (this.offpostlist.length > 0) {
          this.offpostlist.forEach(element => {
            element.check = true;
          })


        }
      }
    },
    invertCheck() {

      if (this.seen == 'OK') {
        if (this.onpostlist.length > 0) {
          this.onpostlist.forEach(element => {
            element.check = !element.check;
          })

        }
      }
      else {

        if (this.offpostlist.length > 0) {
          this.offpostlist.forEach(element => {
            element.check = !element.check;
          })


        }
      }
    },
    checkNone() {
      if (this.seen == 'OK') {
        if (this.onpostlist.length > 0) {
          this.onpostlist.forEach(element => {
            element.check = false;
          })

        }
      }
      else {

        if (this.offpostlist.length > 0) {
          this.offpostlist.forEach(element => {
            element.check = false;
          })


        }
      }
    },
    offwork() {


      var onpost_list = []
      var offpost_list = []
      this.onpostlist.forEach((item, index) => {

        if (item.check) {
          offpost_list.push(item)
        }
        else {
          onpost_list.push(item)

        }
      })
      if (offpost_list.length == 0)
        return;
      this.offduty(offpost_list, onpost_list)


    },
    //下班操作
    offduty(obj1, obj2) {

      this.$axios.post('/api/StaffManager/offwork', obj1).then((response) => {
        console.log(response.data)
        if (response.data.flag == 1) {
          console.info('offduty')
          this.onpostlist = obj2
          this.showScrollBox = false
        }
      }).catch(
        function (error) {
          console.log(error)
        }
        )

    },
    //获取线别
    getlinelist() {
      this.$axios.post('/api/StaffManager/getLine', {}).then((response) => {
        console.log('getlinelist')
        if (response.data.flag == 1) {
          console.log("getline")
          this.linelist = response.data.data
          console.log(this.linelist[0])
          this.new_line = this.linelist[0]

        }
      }).catch(
        function (error) {
          console.log(error)
        }
        )

    },
    linecancel() {

      this.transjob = false
    },
    lineselect(new_line) {
      var checkline = [];
      var uncheline = [];
      if (new_line == "") {
        this.promt1 = '线别不能为空'
        this.showtoast1 = true
        return
      }
      if (new_line == this.staffmodel.line) {
        this.promt1 = '调岗线别不能为当前线别'
        this.showtoast1 = true
        return
      }
      console.log(new_line);
      this.onpostlist.forEach(item => {
        if (item.check) {
          item.new_line = new_line
          checkline.push(item)
        }
        else {
          uncheline.push(item)
        }
      });
      if (checkline.length > 0) {
        this.tranpost(checkline, uncheline);
        //调岗
        this.showScrollBox = false
      }
    },
    tranpost(obj1, obj2) {
      this.$axios.post('/api/StaffManager/tranpost', obj1).then((response) => {
        console.log(response.data)
        if (response.data.flag == 1) {
          console.info('tranpost')
          this.onpostlist = obj2
          this.transjob = false
        }
      }).catch(
        function (error) {
          console.log(error)
        }
        )

    },
    offpost(obj1, obj2) {
      this.$axios.post('/api/StaffManager/offpost', obj1).then((response) => {
        console.log(response.data)
        if (response.data.flag == 1) {
          console.info('offpost')
          this.onpostlist = obj2
          this.showScrollBox = false
        }
      }).catch(
        function (error) {
          console.log(error)
        }
        )


    },
    returnpost() {
      var returnpostlist = []
      var non_returnpostlist = []
      this.offpostlist.forEach(element => {
        if (element.check) {

          returnpostlist.push(element);
        }
        else {
          non_returnpostlist.push(element)
        }
      })
      this.rtpost(returnpostlist, non_returnpostlist)
    },
    rtpost(obj1, obj2) {

      this.$axios.post('/api/StaffManager/returnpost', obj1).then((response) => {
        console.log(response.data)
        if (response.data.flag == 1) {
          console.info('returnpost')
          this.offpostlist = obj2

        }
      }).catch(
        function (error) {
          console.log(error)
        }
        )



    },
    getoffreasonlist() {
      this.$axios.post('/api/StaffManager/getoffreasonlist', {}).then((response) => {

        console.log(response.data)
        if (response.data.flag == 1) {
          console.info('test')
          this.reasonlist = response.data.data

        }
      }).catch(
        function (error) {
          console.log(error)
        }
        )
    },
    ligangcancel() {
      this.showScrollBox = false
    },
    ligangselect(obj) {
      var liganglist = []
      var non_liganglist = []
      if (obj == "") {
        this.promt = '离岗原因不能为空'
        this.showtoast = true
        return
      }
      this.onpostlist.forEach(element => {
        if (element.check) {
          element.ligang_mark = obj
          liganglist.push(element);
        }
        else {
          non_liganglist.push(element)
        }
      })
      console.log("ligangselect")
      if (liganglist.length > 0)
        this.offpost(liganglist, non_liganglist)

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
      onpostlist: [],
      onpost_num: 0,
      offpost_num: 0,
      offpostlist: [],
      showScrollBox: false,
      reasonlist: [],
      ligang_mark: '',
      transjob: false,
      seen: 'OK',
      linelist: [],
      new_line: '',
      showtoast: '',
      promt: '',
      showtoast1: '',
      promt1: '',
      showMenus:false,
      menus: {
        delete: '<span style=" color:red ">退出</span>'
      }

    }

  }

}
</script>


