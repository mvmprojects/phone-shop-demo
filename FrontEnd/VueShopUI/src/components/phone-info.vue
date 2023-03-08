<script setup lang="ts">

const props = defineProps(['phone'])

function handleClick(){

}

</script>

<template>
<div id="bordered-info" class="flex-justify-left">
    <div class="flex-container-row">
        <p style="font-size: xx-large; padding: 1em;">X</p>
        <div class="flex-container-column">
            <h3>
            {{ phone.type }} - {{ phone.brand.name }}
            </h3>
            <div class="info-container">
                <p v-if="phone.price">
                    Price: {{ phone.price }}
                </p>
                <p v-if="phone.stock">
                    Stock: {{ phone.stock }}
                </p>
            </div> 
            <button id="link-button" @click="handleClick">Show more</button>
            <!-- <router-link to="/details">Show more</router-link> -->
        </div>
    </div>
</div>
</template>

<style>
.info-container {
    display: flex;
    flex-direction: row;
    font-size: small;
}

.info-container>*:first-child {
    padding-right: 1em;
}

#bordered-info {
    border-style: solid;
    border-width: thin;
    border-radius: 5px;
    max-width: 20em;
}

#link-button {
    padding: 0.5em;    
    margin-left: auto;
    margin-top: auto;
}
</style>