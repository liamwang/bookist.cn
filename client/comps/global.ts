import Content from './Content'

import {
  Button,
  // Optional
  // Select,
  // Alert,
  // Checkbox,
  // DatePicker,
  // Radio,
  // Switch,
  // Card,
  // List,
  // Tabs,
  // Descriptions,
  // Tree,
  // Table,
  // Divider,
  // Modal,
  // Drawer,
  // Dropdown,
  // Tag,
  // Tooltip,
  // Badge,
  // Popover,
  // Upload,
  // Transfer,
  // Steps,
  // PageHeader,
  // Result,
  // Empty,
  // Avatar,
  // Menu,
  // Breadcrumb,
  // Form,
  // Input,
  // Row,
  // Col,
  // Spin,
} from 'ant-design-vue'

import { App } from 'vue'

const compList = [Content]

export function regGlobalComponents(app: App) {
  compList.forEach((comp: any) => {
    app.component(comp.name, comp)
  })

  app.use(Button)
  //   .use(Select)
  //   .use(Alert)
  //   .use(Breadcrumb)
  //   .use(Checkbox)
  //   .use(DatePicker)
  //   .use(Radio)
  //   .use(Switch)
  //   .use(Card)
  //   .use(List)
  //   .use(Descriptions)
  //   .use(Tree)
  //   .use(Table)
  //   .use(Divider)
  //   .use(Modal)
  //   .use(Drawer)
  //   .use(Dropdown)
  //   .use(Tag)
  //   .use(Tooltip)
  //   .use(Badge)
  //   .use(Popover)
  //   .use(Upload)
  //   .use(Transfer)
  //   .use(Steps)
  //   .use(PageHeader)
  //   .use(Result)
  //   .use(Empty)
  //   .use(Avatar)
  //   .use(Menu)
  //   .use(Tabs)
  //   .use(Form)
  //   .use(Input)
  //   .use(Row)
  //   .use(Col)
  //   .use(Spin);
}
