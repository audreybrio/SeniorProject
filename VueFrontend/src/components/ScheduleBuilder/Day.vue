<template>
    <td>
        <div v-for="item in Items" :key="item.id">
            <Item :item="item" />
        </div>
    </td>
</template>

<script>
import Item from "./Item"
export default {
    props: {
        name: String,
        fullName: String,
        index: Number,
    },
    components:{
        Item,
    },
    methods: {
        compareStartTimes(a, b)
        {
            // Return -1 if a is before b
            // Return  0 if a is at the same time as b
            // Return  1 if a is after b
            if (a.startHour < b.startHour)
            {
                return -1;
            }
            else if (a.startHour == b.startHour)
            {
                if (a.startMinute < b.startMinute)
                {
                    return -1;
                }
                else if (a.startMinute == b.startMinute)
                {
                    return 0;
                }
                else if (a.startMinute > b.startMinute)
                {
                    return 1;
                }
            }
            else if (a.startHour > b.startHour)
            {
                return 1;
            }
        }
    },
    computed: {
        getItemsForDay() {
            return this.items.filter(item => (item.days[this.index] || item.editing))
        },
        getSortedItems() {
            let items = this.getItemsForDay
            // TODO: sort
            return items
        },
        Items(){
            return this.getSortedItems
        }
    },
    data() {
        return {
            items: []
        }
    },
}
</script>

<style scoped>
    div
    {
        border-width: 1px;
        border-style: solid;
        border-color: black;
        list-style: none;
    }
</style>