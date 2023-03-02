<script setup lang="ts">

const props = defineProps(['phone'])

function handleClick(){

}

</script>

<template>
    <h3>
        {{ phone.type }} - {{ phone.brand.name }}
    </h3>
    <div class="info-container-details">
        <p v-if="phone.price">
            Price: {{ phone.price }}
        </p>
        <p v-if="phone.stock">
            Stock: {{ phone.stock }}
        </p>
    </div> 
    <!-- <button @click="handleClick">Show more</button> -->
    <router-link to="/details">Show more</router-link>
</template>

<style>
.info-container-details {
    display: flex;
    flex-direction: row;
    align-items: flex-start;
    font-size: small;
}
</style>