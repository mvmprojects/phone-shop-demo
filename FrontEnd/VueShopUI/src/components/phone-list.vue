<script setup lang="ts">
import PhoneInfo from './phone-info.vue';

let defaultArr = new Array(3);

for (let i = 0; i < defaultArr
.length; i++) {
    defaultArr[i] =       
    {
        id: 0,
        brand: { id: 0, name: 'Default brand' },        
        type: 'Default type',
        price: 10,
        stock: 1,
        description: 'Default description.'
    }
}

</script>

<template>
    <ul>
        <li v-for="item in defaultArr">
            <PhoneInfo :phone="item"></PhoneInfo>
        </li>
    </ul>
</template>

<style scoped>
ul {
    list-style: none;
}
li {
    padding-bottom: 1em;
}
</style>