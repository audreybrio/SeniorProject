<template>
    <td>
        <div v-for="item in Items" :key="item.id">
            <Items
                   :item="item"
                   @item-updated="updateItem"
                   @item-deleted="deleteItem"
                   :editable="editableItems"
            />
        </div>
    </td>
</template>

<script>
    import Items from "./Items"
    export default {
        name: "ScheduleDays",
        props: {
            name: String,
            fullName: String,
            items: Array,
            index: Number,
            editableItems: Boolean
        },
        components:{
            Items,
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
            },
            updateItem(updatedItem) {
                console.log("Days.updateItem()");
                this.$emit('item-updated', updatedItem);
            },
            deleteItem(deleteableItem) {
                console.log("Days.deleteItem()");
                this.$emit("item-deleted", deleteableItem);
            }
        },
        computed: {
            getItemsForDay() {
                return this.items.filter(item => (item.daysOfWeek[this.index] || item.editing));
            },
            getSortedItems() {
                let items = this.getItemsForDay;
                // TODO: sort
                return items;
            },
            Items(){
                return this.getSortedItems;
            }
        }
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