import { defineComponent, renderSlot } from 'vue'

export default defineComponent({
  name: 'Content',
  setup(props, { slots }) {
    return () => (
      <section
        class="content"
        style={{ margin: '16px', padding: '16px', background: '#ffffff' }}
      >
        {renderSlot(slots, 'default')}
      </section>
    )
  },
})
